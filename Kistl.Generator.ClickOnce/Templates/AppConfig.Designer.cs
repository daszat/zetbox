using System.Collections.Generic;
using Kistl.API.Server;


namespace Kistl.Generator.ClickOnce.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator.ClickOnce\Templates\AppConfig.cst")]
    public partial class AppConfig : Kistl.Generator.ResourceTemplate
    {
		protected Kistl.API.IKistlContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("AppConfig", ctx);
        }

        public AppConfig(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 8 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\AppConfig.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
this.WriteObjects("<configuration>\r\n");
this.WriteObjects("  <configSections>\r\n");
this.WriteObjects("    <section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler, log4net\" />\r\n");
this.WriteObjects("  </configSections>\r\n");
this.WriteObjects("  <system.serviceModel>\r\n");
this.WriteObjects("    <!-- Binding -->\r\n");
this.WriteObjects("    <bindings>\r\n");
this.WriteObjects("      <wsHttpBinding>\r\n");
this.WriteObjects("        <binding name=\"KistlService_Binding\" closeTimeout=\"00:01:00\"\r\n");
this.WriteObjects("				 openTimeout=\"00:01:00\" receiveTimeout=\"10:00:00\" sendTimeout=\"00:01:00\"\r\n");
this.WriteObjects("				 bypassProxyOnLocal=\"true\" transactionFlow=\"false\" hostNameComparisonMode=\"StrongWildcard\"\r\n");
this.WriteObjects("				 maxBufferPoolSize=\"524288\" maxReceivedMessageSize=\"6553600\" messageEncoding=\"Text\"\r\n");
this.WriteObjects("				 textEncoding=\"utf-8\" useDefaultWebProxy=\"true\" allowCookies=\"false\">\r\n");
this.WriteObjects("          <security mode=\"Message\">\r\n");
this.WriteObjects("            <message clientCredentialType=\"Windows\" />\r\n");
this.WriteObjects("          </security>\r\n");
this.WriteObjects("          <readerQuotas maxStringContentLength=\"1600000000\" maxArrayLength=\"1600000000\" />\r\n");
this.WriteObjects("          <reliableSession enabled=\"false\" />\r\n");
this.WriteObjects("        </binding>\r\n");
this.WriteObjects("      </wsHttpBinding>\r\n");
this.WriteObjects("    </bindings>\r\n");
this.WriteObjects("    <!-- Client -->\r\n");
this.WriteObjects("    <client>\r\n");
this.WriteObjects("      <endpoint address=\"",  GetServerAddress() , "\" binding=\"wsHttpBinding\"\r\n");
this.WriteObjects("			 bindingConfiguration=\"KistlService_Binding\" contract=\"KistlService.IKistlService\"\r\n");
this.WriteObjects("			 name=\"KistlService_Endpoint\" />\r\n");
this.WriteObjects("    </client>\r\n");
this.WriteObjects("  </system.serviceModel>\r\n");
this.WriteObjects("  <log4net>\r\n");
this.WriteObjects("    <appender name=\"ColoredConsoleAppender\" type=\"log4net.Appender.ColoredConsoleAppender\" >\r\n");
this.WriteObjects("      <mapping>\r\n");
this.WriteObjects("        <level value=\"DEBUG\" />\r\n");
this.WriteObjects("        <foreColor value=\"Cyan\" />\r\n");
this.WriteObjects("      </mapping>\r\n");
this.WriteObjects("      <mapping>\r\n");
this.WriteObjects("        <level value=\"INFO\" />\r\n");
this.WriteObjects("        <foreColor value=\"White\" />\r\n");
this.WriteObjects("      </mapping>\r\n");
this.WriteObjects("      <mapping>\r\n");
this.WriteObjects("        <level value=\"WARN\" />\r\n");
this.WriteObjects("        <foreColor value=\"Yellow, HighIntensity\" />\r\n");
this.WriteObjects("      </mapping>\r\n");
this.WriteObjects("      <mapping>\r\n");
this.WriteObjects("        <level value=\"ERROR\" />\r\n");
this.WriteObjects("        <foreColor value=\"Red, HighIntensity\" />\r\n");
this.WriteObjects("      </mapping>\r\n");
this.WriteObjects("      <mapping>\r\n");
this.WriteObjects("        <level value=\"FATAL\" />\r\n");
this.WriteObjects("        <foreColor value=\"Purple, HighIntensity\" />\r\n");
this.WriteObjects("      </mapping>\r\n");
this.WriteObjects("      <layout type=\"log4net.Layout.PatternLayout\">\r\n");
this.WriteObjects("        <conversionPattern value=\"%message%newline\" />\r\n");
this.WriteObjects("      </layout>\r\n");
this.WriteObjects("    </appender>\r\n");
this.WriteObjects("    <appender name=\"TraceAppender\" type=\"log4net.Appender.TraceAppender\">\r\n");
this.WriteObjects("      <layout type=\"log4net.Layout.PatternLayout\">\r\n");
this.WriteObjects("        <conversionPattern value=\"%date [%thread] %-5level %logger [%property{NDC}] - %message%newline\" />\r\n");
this.WriteObjects("      </layout>\r\n");
this.WriteObjects("    </appender>\r\n");
this.WriteObjects("    <appender name=\"RollingLogFileAppender\" type=\"log4net.Appender.RollingFileAppender\">\r\n");
this.WriteObjects("      <file value=\"Kistl.Client.WPF.log\" />\r\n");
this.WriteObjects("      <appendToFile value=\"true\" />\r\n");
this.WriteObjects("      <rollingStyle value=\"Size\" />\r\n");
this.WriteObjects("      <maxSizeRollBackups value=\"5\" />\r\n");
this.WriteObjects("      <maximumFileSize value=\"1MB\" />\r\n");
this.WriteObjects("      <layout type=\"log4net.Layout.PatternLayout\">\r\n");
this.WriteObjects("        <conversionPattern value=\"%date [%thread] %-5level %logger [%property{NDC}] - %message%newline\" />\r\n");
this.WriteObjects("      </layout>\r\n");
this.WriteObjects("    </appender>\r\n");
this.WriteObjects("    <root>\r\n");
this.WriteObjects("      <level value=\"INFO\" />\r\n");
this.WriteObjects("      <appender-ref ref=\"ColoredConsoleAppender\" />\r\n");
this.WriteObjects("      <appender-ref ref=\"TraceAppender\" />\r\n");
this.WriteObjects("      <appender-ref ref=\"RollingLogFileAppender\" />\r\n");
this.WriteObjects("      <!-- <appender-ref ref=\"RemotingAppender\" /> -->\r\n");
this.WriteObjects("    </root>\r\n");
this.WriteObjects("    <logger name=\"Kistl.Facade\">\r\n");
this.WriteObjects("      <level value=\"DEBUG\" />\r\n");
this.WriteObjects("    </logger>\r\n");
this.WriteObjects("    <logger name=\"Kistl.Linq\">\r\n");
this.WriteObjects("      <level value=\"WARN\" />\r\n");
this.WriteObjects("    </logger>\r\n");
this.WriteObjects("    <logger name=\"Kistl.Reflection\">\r\n");
this.WriteObjects("      <level value=\"WARN\" />\r\n");
this.WriteObjects("    </logger>\r\n");
this.WriteObjects("  </log4net>\r\n");
this.WriteObjects("</configuration>");

        }

    }
}