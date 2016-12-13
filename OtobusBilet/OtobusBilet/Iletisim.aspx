<%@ Page Title="" Language="C#" MasterPageFile="/master/site.Master" AutoEventWireup="true" CodeBehind="Iletisim.aspx.cs" Inherits="OtobusBiletSatis.iletisim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>İletişim</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content2">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <img src="/content/images/sablon/iletisimm.png" />
                <h1>Adres ve Telefonlar</h1>
                <div class="column-1">
                    <div class="column-1 solaYasla">
                        Şehir:<br />
                        <asp:DropDownList ID="dlSubeSehir" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlSubeSehir_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="column-1 solaYasla">
                        İlçe:<br />
                        <asp:DropDownList ID="dlSubeIlce" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlSubeIlce_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="clear"></div>
                </div>
               
                <div class="column-1">
                    <asp:DataList ID="dlSube" runat="server">
                        <ItemTemplate>
                            <div class="seferSatir">
                                <div class="seferSutun"><b>Mail:</b> <%# Eval("mail") %></div>
                                <div class="seferSutun"><b>Sube ID:</b>  <%# Eval("subeID") %></div>
                                <div class="seferSutun"><b>Ad(İLÇE):</b>  <%# Eval("ad") %></div>
                                <div class="seferSutun"><b>Telefon:</b> <%# Eval("telefon") %></div>

                                <div class="clear"></div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <img src="/content/images/sablon/cagri.png" />
        <br />
        <div>E-posta Adresimiz:dis@dis.com.tr  </div>
        <br />
        <div>Genel Müdürlük Adresimiz: İTÜ/AYAZAĞA </div>
        <br />
        <br />
        <iframe src="https://www.google.com/maps/d/embed?mid=zpcjWst0rry0.k0i7XoQLBPQ0" width="785" height="480"></iframe>




    </div>
</asp:Content>
