<%@ Page Title="" Language="C#" MasterPageFile="~/master/site.Master" AutoEventWireup="true" CodeBehind="SifremiUnuttum.aspx.cs" Inherits="OtobusBiletSatis.Unuttum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Şifremi Unuttum</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content2">
        <div class="column-1">
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
                    <asp:AsyncPostBackTrigger ControlID="btnGonder" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>

            <div class="column-1">
                Kullanıcı Adı
                 <div class="column-1">
                     <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                 </div>
            </div>
            <div class="column-1">&</div>
            <div class="column-1">
                Kimlik Numarasi
             <div class="column-1">
                 <asp:TextBox ID="txtTC" runat="server"></asp:TextBox>
             </div>
            </div>
            <div class="column-1">&</div>
            <div class="column-1">
                E-posta
     <div class="column-1">
         <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
     </div>
            </div>
            <div class="column-1">&nbsp;</div>
            <div class="column-1">
                Şifre Alma Yolu<br />
                <asp:RadioButton ID="rb" runat="server" Checked="true" Text="E-Posta" />
            </div>
            <div class="column-1">&nbsp;</div>
            <asp:Button ID="btnGonder" runat="server" Text="Gonder" CssClass="buton" Style="margin-left: 50px;" OnClick="btnGonder_Click" />
            <br />
            <div class="clear"></div>
        </div>
    </div>

    <%--        <div class="contentt">
                <div class="IKresim"><img src="/content/images/site/shatirlat.png"  width="300"/></div>
        </div> --%>
</asp:Content>
