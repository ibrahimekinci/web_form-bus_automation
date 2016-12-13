<%@ Page Title="" Language="C#" MasterPageFile="/master/menulu.master" AutoEventWireup="true" CodeBehind="Biletlerim.aspx.cs" Inherits="OtobusBiletSatis.Biletlerim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headAlt" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bilet" runat="server">
    <asp:UpdatePanel ID="upMesaj" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="column-1">
                <div id="divMesaj" runat="server">
                    <div class="solaYasla dlHeader">Mesaj Kutusu </div>
                    <div class="clear"></div>
                    <div class="mesaj">
                        <div class="baslik"><%= mesajTitle %></div>
                        <p><%= mesajText %></p>
                        <div class="clear"></div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </ContentTemplate>
        <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlBiletlerim" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
              
                    <asp:DataList ID="dlBiletlerim" runat="server" OnItemCommand="dlBiletlerim_ItemCommand">
                        <ItemTemplate>
                            <div class="seferSatir">
                                <div class="seferSutun">
                                   <asp:Button ID="btnBiletSec" runat="server" Text='<%# Eval("btnText") %>' CommandArgument='<%# Eval("biletID") %>' CommandName='<%# Eval("durum") %>' CssClass="btnSeferSecGidis" />
                                </div>
                                <div class="seferSutun"><b>Bilet ID:</b>  <%# Eval("biletID") %></div>
                                <div class="seferSutun"><b>Sefer:</b> <%# Eval("ad") %></div>
                                <div class="seferSutun"><b>İşlem:</b> <%# Eval("islem") %></div>
                                <div class="seferSutun"><b>Ucret(TL):</b> <%# Eval("ucret") %></div>
                                <div class="seferSutun"><b>Tarih:</b> <%# Eval("yolculukTarihi") %></div>
                                 <div class="seferSutun"><b>Durum:</b> <%# Eval("durum") %></div>
                                <div class="seferSutun" >
                                   <asp:Button ID="btnBiletKes" runat="server" Text='Al' CommandArgument='<%# Eval("biletID") %>' CommandName=<%# Eval("islem") %> CssClass="btnSeferSecGidis" />
                                </div>
                                   <div class="clear"></div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
               
               </ContentTemplate>
        <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlBiletlerim" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
