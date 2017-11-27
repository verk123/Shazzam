﻿namespace ShazzamUnitTests.CodeGen
{
    using System;
    using System.Collections.Generic;

    using Microsoft.CSharp;

    using NUnit.Framework;

    using Shazzam;
    using Shazzam.CodeGen;

    public class ShaderClassTests
    {
        [Test]
        public void GetSourceTextPixelShaderCtor()
        {
            var shaderModel = new ShaderModel(
                shaderFileName: "Foo.cs",
                generatedClassName: "Foo",
                generatedNamespace: "Shaders",
                description: "This is Foo",
                targetFramework: TargetFramework.WPF,
                registers: new List<ShaderModelConstantRegister>
                    {
                        ShaderModelConstantRegister.Create(
                            registerName: "Bar",
                            registerType: typeof(double),
                            registerNumber: 1,
                            summary: "This is Bar",
                            minValue: null,
                            maxValue: null,
                            defaultValue: 0)
                    });

            var actual = ShaderClass.GetSourceText(new CSharpCodeProvider(), shaderModel, includePixelShaderConstructor: true);
            Console.Write(actual);
            var expected = @"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace Shaders
{
    
    /// <summary>This is Foo</summary>
    public class Foo : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(""Input"", typeof(Foo), 0);

        public static readonly DependencyProperty BarProperty = DependencyProperty.Register(""Bar"", typeof(double), typeof(Foo), new UIPropertyMetadata(0D, PixelShaderConstantCallback(1)));

        public Foo(PixelShader shader)
        {
            this.PixelShader = shader;
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(BarProperty);
        }

        /// <summary>
        /// There has to be a property of type Brush called ""Input"". This property contains the input image and it is usually not set directly - it is set automatically when our effect is applied to a control.
        /// </summary>
        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }

        /// <summary>This is Bar</summary>
        public double Bar
        {
            get
            {
                return ((double)(this.GetValue(BarProperty)));
            }
            set
            {
                this.SetValue(BarProperty, value);
            }
        }
    }
}

";
            Assert.AreEqual(expected.Replace("\r", string.Empty), actual.Replace("\r", string.Empty));
        }

        [Test]
        public void GetSourceTextDefaultCtor()
        {
            var shaderModel = new ShaderModel(
                shaderFileName: "Foo.cs",
                generatedClassName: "Foo",
                generatedNamespace: "Shaders",
                description: "This is Foo",
                targetFramework: TargetFramework.WPF,
                registers: new List<ShaderModelConstantRegister>
                               {
                                   ShaderModelConstantRegister.Create(
                                       registerName: "Bar",
                                       registerType: typeof(double),
                                       registerNumber: 1,
                                       summary: "This is Bar",
                                       minValue: null,
                                       maxValue: null,
                                       defaultValue: 0)
                               });

            var actual = ShaderClass.GetSourceText(new CSharpCodeProvider(), shaderModel, includePixelShaderConstructor: false);
            Console.Write(actual);
            var expected = @"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace Shaders
{
    
    /// <summary>This is Foo</summary>
    public class Foo : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(""Input"", typeof(Foo), 0);

        public static readonly DependencyProperty BarProperty = DependencyProperty.Register(""Bar"", typeof(double), typeof(Foo), new UIPropertyMetadata(0D, PixelShaderConstantCallback(1)));

        /// <summary>
        /// The uri should be something like pack://application:,,,/Gu.Wpf.Geometry;component/Effects/Foo.ps
        /// The file Foo.ps should have BuildAction: Resource
        /// </summary>
        private static readonly PixelShader Shader = new PixelShader
        {
            UriSource = new Uri(""pack://application:,,,/[assemblyname];component/[folder]/Foo.ps"", UriKind.Absolute)
        };

        public Foo()
        {
            this.PixelShader = Shader;
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(BarProperty);
        }

        /// <summary>
        /// There has to be a property of type Brush called ""Input"". This property contains the input image and it is usually not set directly - it is set automatically when our effect is applied to a control.
        /// </summary>
        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }

        /// <summary>This is Bar</summary>
        public double Bar
        {
            get
            {
                return ((double)(this.GetValue(BarProperty)));
            }
            set
            {
                this.SetValue(BarProperty, value);
            }
        }
    }
}

";
            Assert.AreEqual(expected.Replace("\r", string.Empty), actual.Replace("\r", string.Empty));
        }

        [Test]
        public void CompileInMemory()
        {
            var shaderModel = new ShaderModel(
                shaderFileName: "Foo.cs",
                generatedClassName: "Foo",
                generatedNamespace: "Shaders",
                description: "This is Foo",
                targetFramework: TargetFramework.WPF,
                registers: new List<ShaderModelConstantRegister>
                {
                    ShaderModelConstantRegister.Create(
                        registerName: "Bar",
                        registerType: typeof(double),
                        registerNumber: 1,
                        summary: "This is Bar",
                        minValue: null,
                        maxValue: null,
                        defaultValue: 0)
                });

            var code = ShaderClass.GetSourceText(new CSharpCodeProvider(), shaderModel, includePixelShaderConstructor: false);
            Assert.NotNull(ShaderClass.CompileInMemory(code));
        }
    }
}
