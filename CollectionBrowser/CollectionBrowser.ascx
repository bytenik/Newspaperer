<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CollectionBrowser.ascx.cs"
    Inherits="Windchime.CollectionBrowser" %>
<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
    TagPrefix="NineRays" %>
<asp:UpdatePanel runat="server" ID="Browser_upnl">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Collections_tree" />
    </Triggers>
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <NineRays:FlyTreeView Width="175px" PostBackOnDropAccept="true" ID="Collections_tree" runat="server" ImageSet="Vista" OnPopulateNodes="Collections_PopulateNodes"
                        OnSelectedNodeChanged="Collections_SelectedNodeChanged" BackColor="White" DragDropMode="CtrlDefinedMoveDefault"
                        DrawLines="False" ScrollLeft="0" ScrollTop="0" PostBackOnExpand="true" PostBackOnSelect="True"
                        SelectNodeOnRightClick="False" DragDropName="Collections_tree" DragDropAcceptNames="Collections_tree,Collection,Asset"
                        RootDragDropAcceptNames="Collections_tree,Collection">
                        <HoverStyle Font-Underline="True" />
                        <DefaultStyle Font-Names="Tahoma" Font-Size="11px" ForeColor="Black" ImageUrl="$vista_folder"
                            Padding="3px;7px;7px;3px" RowHeight="18px" />
                        <SelectedStyle BackColor="#D6F0FD" BorderColor="#9ADFFE" BorderStyle="Solid" BorderWidth="1px"
                            Padding="2px;6px;6px;2px" />
                    </NineRays:FlyTreeView>
                </td>
                <td valign="top" rowspan="2">
                    <div runat="server" class="inspector" id="CollectionInspector_pnl" onmouseover="flytreeview_dragOver('Collections_tree', 'DoStuff')" onmouseout="flytreeview_dragOut()">
                        <asp:ListView ID="CollectionInspector_Collections_lvw" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:LinkButton>
                                <div onmousedown="flytreeview_startDrag('Collection', '<%# Eval("ID") %>', '<%# Eval("Name") %>');">
                                    <asp:Image runat="server" ImageUrl='<%# "~/Images/" + Eval("Image") %>' />
                                    <p><%# Eval("Name") %></p>
                                </div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:ListView ID="CollectionInspector_Assets_lvw" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:LinkButton>
                                    <div onmousedown="flytreeview_startDrag('Asset', '<%# Eval("ID") %>', '<%# Eval("Name") %>');">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Images/" + Eval("Image") %>' />
                                        <p>
                                            <%# Eval("Name") %></p>
                                    </div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Filters go here
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
