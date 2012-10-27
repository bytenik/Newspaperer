<%@ Page Title="" Language="C#" MasterPageFile="~/Backend.Master" AutoEventWireup="true" CodeBehind="TextAsset.aspx.cs" Inherits="Windchime.TextAsset" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <p>
        <h5>Asset Name</h5>
        <asp:TextBox ID="Name_box" runat="server" CssClass="block h2-size" />
        <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="Name_box" WatermarkCssClass="watermarked h2-size block"
            WatermarkText="Type a name for the asset..." />
    </p>
    <p>
        <h5>Content</h5>
        <asp:TextBox ID="Content_box" runat="server" CssClass="block tall" TextMode="MultiLine" />
        <ajax:TextBoxWatermarkExtender ID="Content_TextBoxWatermarkExtender" runat="server"
            WatermarkCssClass="watermarked block tall" WatermarkText="Type content for the asset..."
            TargetControlID="Content_box" />
    </p>
    <p>
        <asp:Panel ID="Children_panel" runat="server" ScrollBars="Vertical" />
        <p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Save_btn" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="Save_btn" Text="Save" runat="server" />
            <asp:Button ID="SaveQuit_btn" Text="Save & Return" runat="server" />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Saving" ImageUrl="~/Images/ajax-loading.gif" /></ProgressTemplate>
            </asp:UpdateProgress>
        </p>
    </p>
</asp:Content>
