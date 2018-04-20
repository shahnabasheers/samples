// System.Web.Services.Protocols.LogicalMethodInfo.BeginInvoke(object,object[],AsyncCallback,object) 
// System.Web.Services.Protocols.LogicalMethodInfo.EndInvoke(object,IAsyncResult)

/* The following example demonstrates 'BeginInvoke' and 'EndInvoke' methods of
   'LogicalMethodInfo' class. The 'Add' method of Math web service is called in asynchronous mode. 'BeginInvoke'
   begins asynchronous invocation of method and 'EndInvoke' terminates the invocation 
   started by 'BeginInvoke'. The return value returned by 'Endinvoke' is displayed.
   
   Note:  The MyMath class is a proxy class generated by the Wsdl.exe utility for
   the Math Web Service. This class can also be found in SoapHttpClientProtocol Class example. 
*/

using System;
using System.Reflection;
using System.Web.Services.Protocols;

public class BeginInvokeClass
{
// <Snippet1>
// <Snippet2>
   public static void Main()
   {
      // Get the type information.
      // Note: The MyMath class is a proxy class generated by the Wsdl.exe
      // utility for the Math Web service. This class can also be found in 
      // the SoapHttpClientProtocol class example. 
      Type myType = typeof(MyMath.MyMath);

      // Get the method info.
      MethodInfo myBeginMethod = myType.GetMethod("BeginAdd");
      MethodInfo myEndMethod = myType.GetMethod("EndAdd");

      // Create an instance of the LogicalMethodInfo class.
      LogicalMethodInfo myLogicalMethodInfo = 
         (LogicalMethodInfo.Create(new MethodInfo[] {myBeginMethod,myEndMethod},
         LogicalMethodTypes.Async))[0];

      // Get an instance of the proxy class.
      MyMath.MyMath myMathService = new MyMath.MyMath();

      // Call the MyEndIntimationMethod method to intimate the end of 
      // the asynchronous call.
      AsyncCallback myAsyncCallback = new AsyncCallback(MyEndIntimationMethod);

      // Begin to invoke the Add method.
      IAsyncResult myAsyncResult = myLogicalMethodInfo.BeginInvoke(
         myMathService,new object[]{10,10},myAsyncCallback,null);
      
      // Wait until invoke is complete.
      myAsyncResult.AsyncWaitHandle.WaitOne();
      
      // Get the result.
      object[] myReturnValue;
      myReturnValue = myLogicalMethodInfo.EndInvoke(myMathService,myAsyncResult);
      
      Console.WriteLine("Sum of 10 and 10 is " + myReturnValue[0]);
   }
   
   // This method will be called at the end of the asynchronous call.
   static void MyEndIntimationMethod(IAsyncResult Result)
   {
      Console.WriteLine("Asynchronous call on Add method finished.");
   }
// </Snippet1>
// </Snippet2>
}

// Automatically generated proxy class for Math Web service.
// This class can also be found in the SoapHttpClientProtocol class example.
namespace MyMath 
{
   using System.Diagnostics;
   using System.Xml.Serialization;
   using System;
   using System.Web.Services.Protocols;
   using System.Web.Services;

   [System.Web.Services.WebServiceBindingAttribute(
      Name="MyMathSoap", Namespace="http://tempuri.org/")]
   public class MyMath : System.Web.Services.Protocols.SoapHttpClientProtocol 
   {
      public MyMath() 
      {
         this.Url = "http://localhost/Math.asmx";
      }
    
      [System.Web.Services.Protocols.SoapDocumentMethodAttribute(
         "http://tempuri.org/Add", 
         Use=System.Web.Services.Description.SoapBindingUse.Literal, 
         ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
      public int Add(int x, int y) 
      {
         object[] results = this.Invoke("Add", new object[] {x,y});
         return ((int)(results[0]));
      }
    
      public System.IAsyncResult BeginAdd(int x, int y, 
         System.AsyncCallback callback, object asyncState) 
      {
         return this.BeginInvoke("Add", new object[] {x,y}, callback, asyncState);
      }
    
      public int EndAdd(System.IAsyncResult asyncResult) 
      {
         object[] results = this.EndInvoke(asyncResult);
         return ((int)(results[0]));
      }
   }
}