using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

using System;
using System.Runtime.Serialization;

using System.CodeDom; 
using System.CodeDom.Compiler; 
using Microsoft.CSharp; 
using System.Reflection; 



public class mapEdit : Editor
{
	[MenuItem("SceneMap/Save Map")]
	static void GenerateCode() {

		/*注意，先导入下面的命名空间
		using System.CodeDom
		using System.CodeDom.Compiler;
		using Microsoft.CSharp;
		using System.Reflection;*/
		
		//准备一个代码编译器单元		
		CodeCompileUnit unit = new CodeCompileUnit();		
		//准备必要的命名空间（这个是指要生成的类的空间）		
		CodeNamespace sampleNamespace=new CodeNamespace();		
		//导入必要的命名空间		
		sampleNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine"));		
		sampleNamespace.Imports.Add(new CodeNamespaceImport("System.Collections"));		
		sampleNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));		
		sampleNamespace.Imports.Add(new CodeNamespaceImport("UnityEditor"));		
		//准备要生成的类的定义		
		CodeTypeDeclaration Customerclass = new CodeTypeDeclaration("Customer");		
		//指定这是一个Class		
		Customerclass.IsClass = true;	
		Customerclass.BaseTypes.Add (new CodeTypeReference (typeof(Editor)));
		Customerclass.TypeAttributes = TypeAttributes.Public;// | TypeAttributes.Sealed;

		//把这个类放在这个命名空间下		
		sampleNamespace.Types.Add(Customerclass);		
		//把该命名空间加入到编译器单元的命名空间集合中		
		unit.Namespaces.Add(sampleNamespace);		
		//这是输出文件		
		string outputFile = "Customer.cs";

#if EditTest
		//添加字段
		
		CodeMemberField field = new CodeMemberField(typeof(System.String), "_Id");
		
		field.Attributes = MemberAttributes.Private;
		
		Customerclass.Members.Add(field);
		
		//添加属性
		
		CodeMemberProperty property = new CodeMemberProperty();
		
		property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
		
		property.Name = "Id";
		
		property.HasGet = true;
		
		property.HasSet = true;
		
		property.Type = new CodeTypeReference(typeof(System.String));
		
		property.Comments.Add(new CodeCommentStatement("这是Id属性"));
		
		property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id")));
		
		property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id"), new CodePropertySetValueReferenceExpression()));
		
		Customerclass.Members.Add(property);
#endif

		//添加特征(使用 CodeAttributeDeclaration)
		//CodeAttributeDeclaration dec = new CodeAttributeDeclaration ();
#if false
		Customerclass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SerializableAttribute))));
		CodeTypeReference menuItem = new CodeTypeReference (typeof(MenuItem));
		CodeTypeParameter par = new CodeTypeParameter("xx");	
		Customerclass.CustomAttributes.Add(new CodeAttributeDeclaration(menuItem));
#endif
		//添加方法（使用CodeMemberMethod)--此处略
		CodeMemberMethod method = new CodeMemberMethod();
		method.Attributes = MemberAttributes.Static;
		method.Name = "Scene01";
		method.Parameters.Add (new CodeParameterDeclarationExpression(typeof(string) ,"str"));
		Customerclass.Members.Add(method);
		CodeTypeParameter par = new CodeTypeParameter ();
	

		//CodeTypeReference menuItem = new CodeTypeReference (typeof(MenuItem));
		//CodeTypeReference menuItem = new CodeTypeReference ("MenuItem(\"xxxxx\")");
		//menuItem.TypeArguments.Add(new CodeTypeReference(typeof(MenuItem)));
		//CodeAttributeDeclaration x = new CodeAttributeDeclaration (menuItem);
		//method.CustomAttributes.Add (new CodeAttributeDeclaration ().Name = "xxxx");
		//method.CustomAttributes.Add (new CodeAttributeDeclaration ("MenuItem(\"xxsssxxx\")"));

		//添加构造器(使用CodeConstructor) --此处略
		
		//添加程序入口点（使用CodeEntryPointMethod） --此处略
		
		//添加事件（使用CodeMemberEvent) --此处略
		



		//生成代码
		
		CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
		
		CodeGeneratorOptions options = new CodeGeneratorOptions();
		
		options.BracingStyle = "C";
		
		options.BlankLinesBetweenMembers = true;
		
		using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile)) {
			Debug.Log("自动生成代码");
			provider.GenerateCodeFromCompileUnit(unit, sw, options);
			
		}
		
	}

}
