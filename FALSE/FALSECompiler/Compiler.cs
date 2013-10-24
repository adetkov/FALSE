using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private readonly Dictionary<ILCode.ILType, Action<ILGenerator, ILCode>> Map;

        private FieldBuilder _stackField;

        private FieldBuilder _stackPtrField;

        private LocalBuilder _tmpField;

        public Compiler()
        {
            Map = new Dictionary<ILCode.ILType, Action<ILGenerator, ILCode>>
                {
                    { ILCode.ILType.PopStack, PopStack },
                    { ILCode.ILType.PushStack, PushStack },

                    { ILCode.ILType.LoadNumber, LoadNumber },
                    { ILCode.ILType.WriteLine, WriteString },
                    { ILCode.ILType.WriteNumber, WriteNumber },
                    { ILCode.ILType.ReadChar, ReadChar },
                };
        }

        public void Compile(string name, int stackSize, ProgramContext context)
        {
            var assemblyName = new AssemblyName(name);
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            var module = assembly.DefineDynamicModule(name, name + ".exe");
            var type = module.DefineType("Program", TypeAttributes.Public | TypeAttributes.Class);
            var mainMethod = type.DefineMethod("Main", MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static);
            assembly.SetEntryPoint(mainMethod, PEFileKinds.ConsoleApplication);


            if (context.IsStackUsed)
            {
                _stackField = type.DefineField("stack", typeof(int[]), FieldAttributes.Private | FieldAttributes.Static);

                
 

                _stackPtrField = type.DefineField("ptr", typeof(int), FieldAttributes.Private | FieldAttributes.Static);    
            }
            
            var ctorIl = mainMethod.GetILGenerator();
            
            ctorIl.Emit(OpCodes.Ldc_I4, stackSize);
            ctorIl.Emit(OpCodes.Newarr, typeof(int));
            ctorIl.Emit(OpCodes.Stsfld, _stackField);
            CompileMethod(ctorIl, context.Main);

            type.CreateType();
            assembly.Save(name + ".exe");
        }

        private void CompileMethod(ILGenerator gen, IEnumerable<ILCode> ilCodes)
        {
            _tmpField = gen.DeclareLocal(typeof (int), false);

            foreach (var code in ilCodes)
            {
                Map[code.Type](gen, code);
            }
        }

        private void PushStack(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Stloc_0);
            g.Emit(OpCodes.Ldsfld, _stackField);
            g.Emit(OpCodes.Ldc_I4_0);
            g.Emit(OpCodes.Ldloc_0);
            g.Emit(OpCodes.Stelem_I4);
        }

        private void PopStack(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldsfld, _stackField);
            g.Emit(OpCodes.Ldc_I4_0);
            g.Emit(OpCodes.Ldelem_I4);
        }
    }
}
