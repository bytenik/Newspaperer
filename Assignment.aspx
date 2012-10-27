<%@ Page Language="C#" MasterPageFile="~/Backend.Master" AutoEventWireup="true" CodeBehind="Assignment.aspx.cs"
    Inherits="Windchime.Assignment1" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <p>
        <h5>
            <asp:Label runat="server" AssociatedControlID="Name_box">Headline</asp:Label></h5>
        <asp:TextBox ID="Name_box" runat="server" CssClass="block h2-size" />
        <ajax:TextBoxWatermarkExtender runat="server" TargetControlID="Name_box" WatermarkCssClass="watermarked h2-size block"
            WatermarkText="Type a headline for the assignment..." />
        <asp:RequiredFieldValidator Display="None" ID="Name_validator" ControlToValidate="Name_box" runat="server" ErrorMessage="<strong>Empty required field!</strong><br/>An assignment headline is required!" />
        <ajaxToolkit:ValidatorCalloutExtender TargetControlID="Name_validator" ID="Name_validator_extender" runat="server" />
    </p>
    <p>
        <h5>
            <asp:Label runat="server" AssociatedControlID="Summary_box">Summary</asp:Label></h5>
        <asp:TextBox ID="Summary_box" runat="server" CssClass="block tall" TextMode="MultiLine" />
        <ajax:TextBoxWatermarkExtender ID="Summary_TextBoxWatermarkExtender" runat="server"
            WatermarkCssClass="watermarked block tall" WatermarkText="Type a summary for the assignment..."
            TargetControlID="Summary_box" />
        <asp:RequiredFieldValidator Display="None" ID="Summary_validator" ControlToValidate="Summary_box" runat="server" ErrorMessage="<strong>Empty required field!</strong><br/>An assignment summary is required!" />
        <ajaxToolkit:ValidatorCalloutExtender TargetControlID="Summary_validator" ID="Summary_validator_extender" runat="server" />
    </p>
    <p>
        <h5>
            <asp:Label runat="server" AssociatedControlID="Location_box">Location</asp:Label></h5>
        <asp:TextBox ID="Location_box" runat="server" CssClass="block" />
        <ajax:TextBoxWatermarkExtender ID="Location_TextBoxWatermarkExtender" runat="server"
            TargetControlID="Location_box" WatermarkCssClass="watermarked block" WatermarkText="Type a location for the assignment..." />
    </p>
    <p>
        <h5>
            <asp:Label runat="server" AssociatedControlID="DueDate_box">Due Date</asp:Label></h5>
        <asp:TextBox ID="DueDate_box" runat="server" />
        <ajaxToolkit:CalendarExtender ID="DueDate_CalendarExtender" TargetControlID="DueDate_box"
            runat="server" />
        <ajaxToolkit:TextBoxWatermarkExtender TargetControlID="DueDate_box" WatermarkCssClass="watermarked"
            WatermarkText="Due date" ID="DueDate_BoxExtender" runat="server" />
        <asp:CompareValidator Display="None" ControlToValidate="DueDate_box" Operator="DataTypeCheck" Type="Date" ID="DueDate_validator"
            runat="server" ErrorMessage="<strong>Invalid data entry!</strong><br/>Due date must be valid or empty." />
        <ajaxToolkit:ValidatorCalloutExtender TargetControlID="DueDate_validator" id="DueDate_validator_extender" runat="server" />
    </p>
    <p>
    <h5><asp:label AssociatedControlID="Author_lst">Authors</asp:label></h5>
        <asp:ListBox ID="Author_lst" runat="server" SelectionMode="Multiple" />
    </p>
    <p>
        <asp:CheckBox ID="Completed_chk" runat="server" Text="Completed" />
        <br />
        <asp:CheckBox ID="Approved_chk" runat="server" Text="Approved" />
    </p>
    <p>
        <asp:UpdatePanel runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Save_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="Save_btn" Text="Save" runat="server" CausesValidation="true" />
        <asp:Button ID="SaveQuit_btn" Text="Save & Return" runat="server" CausesValidation="true" />
        <p>
            <asp:UpdateProgress runat="server">
                <ProgressTemplate>
                    <asp:Image runat="server" AlternateText="Saving" ImageUrl="~/Images/ajax-loading.gif" /></ProgressTemplate>
            </asp:UpdateProgress>
        </p>
    </p>
</asp:Content>
