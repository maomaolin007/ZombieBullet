using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Unity.VisualScripting2
{
    public class MyTypeHandleInspector : TypeHandleInspector
    {
        public MyTypeHandleInspector(Metadata metadata)
            : base(metadata) { }

        public override void Initialize()
        {
            base.Initialize();

            var f = typeof(TypeHandleInspector).GetField("m_TypeFilter", System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
            var typeFilter = f.GetValue(this);
            if(typeFilter is TypeFilter type2)
            {
                type2.Interfaces = true;
                type2.Abstract = true;
            }
        }
    }

    public class VSIProvider : InspectorProvider
    {
        public static void Hack()
        {
            var f = typeof(InspectorProvider).GetProperty("instance");
            var inst0 = InspectorProvider.instance;
            f.SetValue(typeof(InspectorProvider), new VSIProvider());
            EditorApplicationUtility.onSelectionChange -= inst0.FreeAll;
            EditorApplicationUtility.onSelectionChange += VSIProvider.instance.FreeAll;
        }

        protected override Type ResolveDecoratorType(Type decoratedType)
        {
            var t = base.ResolveDecoratorType(decoratedType);
            if (t == typeof(TypeHandleInspector))
            {
                return typeof(MyTypeHandleInspector);
            }
            return t;
        }

    }
}
