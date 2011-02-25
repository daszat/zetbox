<%@ Page Language="C#" EnableViewState="false" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected Process p = Process.GetCurrentProcess();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Dictionary<string, int> cacheList = new Dictionary<string, int>();
        foreach (DictionaryEntry c in Cache)
        {
            string type = c.Value != null ? c.Value.GetType().FullName : "NULL";
            if (cacheList.ContainsKey(type))
            {
                cacheList[type]++;
            }
            else
            {
                cacheList[type] = 1;
            }
        }
        repCache.DataSource = cacheList;
        repCache.DataBind();

        repEnvironment.DataSource = Environment.GetEnvironmentVariables();
        repEnvironment.DataBind();

        repHeaders.DataSource = Request.Headers.AllKeys.ToDictionary(k => k, k => Request.Headers.Get(k));
        repHeaders.DataBind();
    }

    protected void OnClearCache(object sender, EventArgs e)
    {
        List<string> keys = new List<string>();
        foreach (DictionaryEntry c in Cache)
        {
            keys.Add(c.Key.ToString());
        }

        foreach (string k in keys)
        {
            Cache.Remove(k);
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        p.Dispose();
    }

    protected string MB(double v)
    {
        return (v / 1024.0 / 1024.0).ToString("n2");
    }

    protected void OnGCCollect(object sender, EventArgs args)
    {
        GC.Collect();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Website Status</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        Web Site Status</h1>
    <div>
        <h2>
            ASP.NET Status</h2>
        <table>
            <tr>
                <td>
                    System Version:
                </td>
                <td>
                    <%= System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion() %>
                </td>
            </tr>
            <tr>
                <td>
                    Runtime Directory:
                </td>
                <td>
                    <%= System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() %>
                </td>
            </tr>
            <tr>
                <td>
                    User.Identity:
                </td>
                <td>
                    <%= User.Identity.Name %>
                </td>
            </tr>
            <tr>
                <td>
                    Thread.CurrentPrincipal:
                </td>
                <td>
                    <%= System.Threading.Thread.CurrentPrincipal.Identity.Name %>
                </td>
            </tr>
            <tr>
                <td>
                    WindowsIdentity:
                </td>
                <td>
                    <%= System.Security.Principal.WindowsIdentity.GetCurrent().Name%>
                </td>
            </tr>
            <tr>
                <td>
                    Total Memory:
                </td>
                <td>
                    <%= MB(GC.GetTotalMemory(false)) %>
                    MB
                </td>
            </tr>
            <tr>
                <td>
                    Mem Usage:
                </td>
                <td>
                    <%= MB(p.WorkingSet64)%>
                    MB
                </td>
            </tr>
            <tr>
                <td>
                    VirtualMemorySize:
                </td>
                <td>
                    <%= MB(p.PagedMemorySize64)%>
                    MB
                </td>
            </tr>
            <tr>
                <td>
                    Cache.EffectivePrivateBytesLimit:
                </td>
                <td>
                    <%= MB(this.Cache.EffectivePrivateBytesLimit)%>
                    MB
                </td>
            </tr>
            <tr>
                <td>
                    Cache.EffectivePercentagePhysicalMemoryLimit:
                </td>
                <td>
                    <%= MB(this.Cache.EffectivePercentagePhysicalMemoryLimit)%>
                    %
                </td>
            </tr>
        </table>
        <asp:Button ID="btnGCCollect" runat="server" OnClick="OnGCCollect" Text="GC.Collect" />
    </div>
    <div>
        <h2>
            Cache</h2>
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
        <asp:Button ID="btnClearCache" runat="server" Text="Clear Cache" OnClick="OnClearCache" />
        <asp:Repeater ID="repCache" runat="server">
            <HeaderTemplate>
                <table border="1" style="border-collapse: collapse;">
                    <tr>
                        <th>
                            Type in Cache
                        </th>
                        <th>
                            Count
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 400px;">
                        <%# Eval("Key") %>
                    </td>
                    <td>
                        <%# Eval("Value") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div>
        <h2>
            Headers</h2>
        <asp:Repeater ID="repHeaders" runat="server">
            <HeaderTemplate>
                <table border="1" style="border-collapse: collapse;">
                    <tr>
                        <th>
                            Key
                        </th>
                        <th>
                            Value
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 400px;">
                        <%# Eval("Key") %>
                    </td>
                    <td>
                        <%# Eval("Value") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div>
        <h2>
            Environment</h2>
        <asp:Repeater ID="repEnvironment" runat="server">
            <HeaderTemplate>
                <table border="1" style="border-collapse: collapse;">
                    <tr>
                        <th>
                            Key
                        </th>
                        <th>
                            Value
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 400px;">
                        <%# Eval("Key") %>
                    </td>
                    <td>
                        <%# Eval("Value") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
