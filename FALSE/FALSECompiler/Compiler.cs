using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private readonly Dictionary<ILCode.ILType, Action<ILGenerator, ILCode>> _map;

        private readonly Dictionary<ILGenerator, Tuple<LocalBuilder, LocalBuilder>> _tmpFields
            = new Dictionary<ILGenerator, Tuple<LocalBuilder, LocalBuilder>>();

        private readonly Dictionary<int, FieldBuilder> _variables = new Dictionary<int, FieldBuilder>();
        private int _funcPtr;
        private FieldBuilder _funcTable;
        private FieldBuilder _stackField;

        private FieldBuilder _stackPtrField;

        public Compiler()
        {
            _map = new Dictionary<ILCode.ILType, Action<ILGenerator, ILCode>>
                {
                    { ILCode.ILType.PopStack, PopStack },
                    { ILCode.ILType.PushStack, PushStack },

                    { ILCode.ILType.LoadNumber, LoadNumber },
                    { ILCode.ILType.WriteLine, WriteString },
                    { ILCode.ILType.WriteNumber, WriteNumber },
                    { ILCode.ILType.WriteChar, WriteChar },
                    { ILCode.ILType.ReadChar, ReadChar },

                    { ILCode.ILType.Negate, Negate },
                    { ILCode.ILType.Add, Add },
                    { ILCode.ILType.Sub, Substract },
                    { ILCode.ILType.Mul, Multiplicate },
                    { ILCode.ILType.Div, Divide },

                    { ILCode.ILType.Not, Not },
                    { ILCode.ILType.Equal, Equal },
                    { ILCode.ILType.And, And },
                    { ILCode.ILType.Or, Or },
                    { ILCode.ILType.Greater, Greater },

                    { ILCode.ILType.Duplicate, Duplicate },
                    { ILCode.ILType.Drop, Drop },
                    { ILCode.ILType.Swap, Swap },

                    { ILCode.ILType.LoadVariable, LoadVariable },
                    { ILCode.ILType.StoreVariable, StoreVariable },

                    { ILCode.ILType.PushFunction, PushFunction },
                    { ILCode.ILType.CallFunction, CallFunction },

                    { ILCode.ILType.Label, Label },
                    { ILCode.ILType.JumpIfFalse, JumpIfFalse },
                    { ILCode.ILType.Jump, Jump },
                };
        }

        public void Compile(string name, int stackSize, ProgramContext context)
        {
            var assemblyName = new AssemblyName(name);
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            var module = assembly.DefineDynamicModule(name, name + ".exe");
            var type = module.DefineType("Program", TypeAttributes.Public | TypeAttributes.Class);

            if (context.IsStackUsed)
            {
                _stackField = CreateField<int>(type, "stack");
                _stackPtrField = CreateField<int[]>(type, "ptr");
            }

            InitVariables(type, context);

            if (context.Funcs.Length > 0)
            {
                _funcTable = CreateField<Action[]>(type, "funcTable");
            }

            var funcs = context.Funcs.Select(f => CreateFunction(type, f)).ToArray();

            var mainMethod = type.DefineMethod("Main", MethodAttributes.HideBySig
                | MethodAttributes.Public | MethodAttributes.Static);
            assembly.SetEntryPoint(mainMethod, PEFileKinds.ConsoleApplication);
            var ctorIl = mainMethod.GetILGenerator();

            ctorIl.Emit(OpCodes.Ldc_I4, stackSize);
            ctorIl.Emit(OpCodes.Newarr, typeof(int));
            ctorIl.Emit(OpCodes.Stsfld, _stackField);

            // Init funcTable

            if (funcs.Length > 0)
            {
                var actionConstructor =
                    Type.GetType("System.Action").GetConstructor(new[] { typeof(object), typeof(IntPtr) });

                ctorIl.Emit(OpCodes.Ldc_I4, funcs.Length);
                ctorIl.Emit(OpCodes.Newarr, typeof(Action));
                ctorIl.Emit(OpCodes.Stsfld, _funcTable);

                for (int i = 0; i < funcs.Length; i++)
                {
                    ctorIl.Emit(OpCodes.Ldsfld, _funcTable);
                    ctorIl.Emit(OpCodes.Ldc_I4, i);
                    ctorIl.Emit(OpCodes.Ldnull);
                    ctorIl.Emit(OpCodes.Ldftn, funcs[i]);
                    ctorIl.Emit(OpCodes.Newobj, actionConstructor);
                    ctorIl.Emit(OpCodes.Stelem_Ref);
                }
            }

            CompileMethod(ctorIl, context.Main.Codes);

            type.CreateType();
            assembly.Save(name + ".exe");
        }

        private static FieldBuilder CreateField<T>(TypeBuilder type, string name)
        {
            return type.DefineField(name, typeof(T), FieldAttributes.Private | FieldAttributes.Static);
        }

        private void CompileMethod(ILGenerator gen, IEnumerable<ILCode> ilCodes)
        {
            _tmpFields[gen] = new Tuple<LocalBuilder, LocalBuilder>(
                gen.DeclareLocal(typeof(int), false),
                gen.DeclareLocal(typeof(int), false));

            foreach (var code in ilCodes)
            {
                _map[code.Type](gen, code);
            }

            gen.Emit(OpCodes.Ret);
        }

        private MethodBuilder CreateFunction(TypeBuilder type, Function function)
        {
            var method = type.DefineMethod("Func_" + _funcPtr++, MethodAttributes.HideBySig
                | MethodAttributes.Public | MethodAttributes.Static);

            var gen = method.GetILGenerator();

            if (function.Codes.Length == 0)
            {
                gen.Emit(OpCodes.Nop);
            }

            CompileMethod(gen, function.Codes);

            return method;
        }

        private void InitVariables(TypeBuilder type, ProgramContext context)
        {
            var variables = context.Variables;
            int cnt = 0;
            for (uint v = 1U; v <= 80000000U; v <<= 1, cnt++)
            {
                if ((variables & v) != 0)
                {
                    _variables[cnt] = CreateField<int>(type, ((char)('a' + cnt)).ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private void PopStack(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldsfld, _stackField);

            // Decrement stack pointer
            g.Emit(OpCodes.Ldsfld, _stackPtrField);
            g.Emit(OpCodes.Ldc_I4_1);
            g.Emit(OpCodes.Sub);
            g.Emit(OpCodes.Dup);
            g.Emit(OpCodes.Stsfld, _stackPtrField);

            g.Emit(OpCodes.Ldelem_I4);
        }

        private void PushStack(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Stloc_0, _tmpFields[g].Item1);
            g.Emit(OpCodes.Ldsfld, _stackField);

            // Icrement stack pointer
            g.Emit(OpCodes.Ldsfld, _stackPtrField);
            g.Emit(OpCodes.Dup);
            g.Emit(OpCodes.Ldc_I4_1);
            g.Emit(OpCodes.Add);
            g.Emit(OpCodes.Stsfld, _stackPtrField);

            g.Emit(OpCodes.Ldloc_0);
            g.Emit(OpCodes.Stelem_I4);
        }
    }
}