<%@ Page Title="" Language="C#" MasterPageFile="/master/site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OtobusBiletSatis.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content2">

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
                <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>

                <div class="column-1" id="divLogin" runat="server">
                    <div class="solaYasla dlHeader">Üye Girişi</div>
                    <div class="clear"></div>
                    <div class="mesaj">
                        <div class="column-1">
                            Kullanıcı Adı
                <div class="column-1">
                    <asp:TextBox ID="txtUser" runat="server" TabIndex="0"></asp:TextBox>
                </div>
                        </div>
                        <div class="column-1">
                            Şifre
                    <div class="column-1">
                        <asp:TextBox ID="txtPass" runat="server" TabIndex="1" TextMode="Password"></asp:TextBox>
                    </div>
                        </div>
                        <div class="column-1">
                            <br />
                            <asp:Button ID="btnGirisYap" runat="server" Text="Giriş Yap" CssClass="buton" TabIndex="2" Style="margin-left: 50px;" OnClick="btnGirisYap_Click" />

                        </div>
                        <div class="column-1">
                            <div style="margin: 5px">
                                <asp:CheckBox ID="cbHatirla" runat="server" Text="Beni Hatırla" ForeColor="Black" TabIndex="3" />
                                <a href="/login/SifremiUnuttum.aspx" target="_blank" tabindex="4">Şifremi Unuttum</a>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
