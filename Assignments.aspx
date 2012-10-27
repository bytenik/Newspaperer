<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Windchime.Assignments"
    Title="Assignment Viewer" MasterPageFile="~/Backend.Master" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div>
        <p>
            <asp:Label ID="Keywords_label" runat="server" Text="Keywords"></asp:Label><br />
            <asp:TextBox ID="Keywords_box" runat="server"></asp:TextBox></p>
        <ajax:TextBoxWatermarkExtender ID="Keywords_watermark" runat="server" TargetControlID="Keywords_box"
            WatermarkText="Keywords" WatermarkCssClass="watermarked" />
        </p>
        <p>
            <asp:Label ID="Issue_label" runat="server" Text="Issue"></asp:Label><br />
            <asp:DropDownList ID="Issue_list" runat="server" />
        </p>
        <p>
            <asp:Label ID="Author_label" runat="server" Text="Author"></asp:Label><br />
            <asp:TextBox ID="Author_box" runat="server"></asp:TextBox>
            <ajaxToolkit:TextBoxWatermarkExtender ID="Author_watermark" runat="server" TargetControlID="Author_box"
                WatermarkText="Author's Name" WatermarkCssClass="watermarked" />
            <ajaxToolkit:AutoCompleteExtender runat="server" TargetControlID="Author_box" MinimumPrefixLength="3"
                ServicePath="StaffAutoComplete.asmx" ServiceMethod="GetCompletionList" />
        </p>
        <p>
            <asp:Button ID="Search_button" runat="server" Text="Search" />
        </p>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <p>
                    <asp:Image ImageUrl="~/Images/ajax-loading.gif" AlternateText="Loading..." runat="server" />
                </p>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <hr />
    <div>
        <asp:UpdatePanel ID="Assignments_panel" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Search_button" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView CssClass="Grid" ID="Assignments_grid" runat="server" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" ShowFooter="true" 
                    DataKeyNames="CollectionID" OnRowCommand="Assignments_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("CollectionID", "Assignment.aspx?ID={0}") %>'>
                                    <asp:Image ImageUrl="~/Images/folder_open_16x16.gif" AlternateText="Open" runat="server" />
                                </asp:HyperLink>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Assignment.aspx">
                                    <asp:Image runat="server" AlternateText="Open New" ImageUrl="~/Images/folder_(add)_16x16.gif" />
                                </asp:HyperLink>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="CollectionID" DataNavigateUrlFormatString="Assignment.aspx?ID={0}"
                            DataTextField="Name" HeaderText="Assignment" SortExpression="Name" />
                        <asp:BoundField DataField="StartDate" HeaderText="Starts" SortExpression="StartDate" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due" SortExpression="DueDate" />
                        <asp:BoundField DataField="Summary" HeaderText="Summary" SortExpression="Summary" />
                        <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                        <asp:BoundField DataField="CompletedDate" HeaderText="Completed" SortExpression="CompletedDate" />
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle CssClass="alternate-row" />
                    <EmptyDataTemplate>
                        <h3>
                            No assignments match the search criteria.</h3>
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
