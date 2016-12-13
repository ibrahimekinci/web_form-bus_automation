<%@ Page Title="" Language="C#" MasterPageFile="/master/site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OtobusBiletSatis.anasayfa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Anasayfa</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="contentAnaSayfa">
        <div class="column-1">

            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
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
                    <asp:AsyncPostBackTrigger ControlID="btnIlet" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubeAra" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="left-Anasayfa">
            <div id="left-menu">
                <div class="anasayfaLeftMenuBack"></div>
                <ul class="contentAnaSayfaLeftMenu">
                    <li>Bilet İşlemleri</li>
                    <li id="menuHizliGiris" runat="server">Hızlı Giriş</li>
                    <a href="/Biletlerim.aspx">
                        <li>Biletlerim</li>
                    </a>
                    <li>Şube ve Terminaller</li>
                    <li>Öneri ve Görüşleriniz</li>

                </ul>
            </div>
            <div id="AnasayfaIcerik">
                <div class="AnaSayfaIcerik">

                    <div class="AnaSayfaBaslik">Nereden</div>
                    <div class="AnaSayfaBaslik">
                        <asp:DropDownList ID="ddlGidis" runat="server">
                            <asp:ListItem>sdsa</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="AnaSayfaBaslik">Nereye</div>
                    <div class="AnaSayfaBaslik">
                        <asp:DropDownList ID="ddlDonus" runat="server"></asp:DropDownList>
                    </div>

                    <div class="AnaSayfaBaslik">Haraket Zamanı</div>
                    <div class="AnaSayfaBaslik">
                        <div class="AnaSayfaBaslik">
                            <div class="takvimSmall"></div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSeferGunu" runat="server"></asp:TextBox>
                                    <div class="clear"></div>

                                    <div class="AnaSayfaBaslik">

                                        <div class="takvimLarge">
                                            <asp:Calendar ID="cldSeferGunu" runat="server" CssClass="AnasayfaTarihSec" OnSelectionChanged="cldSeferGunu_SelectionChanged"></asp:Calendar>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSeferAra" runat="server" Text="Sefer Ara" CssClass="seferAra" OnClick="btnSeferAra_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
                <div class="AnaSayfaIcerik" id="icerikHizliGiris" runat="server">
                    <div class="AnaSayfaBaslik">Hızlı Giriş</div>
                    <div class="column-1">&nbsp;</div>
                    <asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="column-1">
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
                                <div class="column-1">&nbsp;</div>
                                <div class="column-1">

                                    <asp:Button ID="btnGirisYap" runat="server" Text="Giriş Yap" CssClass="buton" TabIndex="2" Style="margin-left: 50px;" OnClick="btnGirisYap_Click" />

                                </div>
                                <div class="column-1">&nbsp;</div>
                                <div class="column-1">

                                    <asp:CheckBox ID="cbHatirla" runat="server" Text="Beni Hatırla" ForeColor="white" TabIndex="3" />
                                </div>
                                <div class="column-1" style="padding-left: 5px;">
                                    <a href="/login/SifremiUnuttum.aspx" tabindex="4" target="_blank">Şifremi Unuttum</a>

                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <div class="AnaSayfaIcerik">
                    <div class="AnaSayfaBaslik">Biletlerim</div>


                </div>
                <div class="AnaSayfaIcerik">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="column-1">
                                Şehir:<br />
                                <asp:DropDownList ID="dlSubeSehir" runat="server" AutoPostBack="True" TabIndex="9" OnSelectedIndexChanged="dlSubeSehir_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="column-1">
                                İlçe:<br />
                                <asp:DropDownList ID="dlSubeIlce" runat="server" AutoPostBack="false" TabIndex="10"></asp:DropDownList>
                            </div>
                            <div class="column-1">
                                <asp:Button ID="btnSubeAra" runat="server" Text="Detayları Goster" OnClick="btnSubeAra_Click" TabIndex="11" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="AnaSayfaIcerik">
                    <h1>Bize mail ile ulaşın</h1>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="column-1">
                                Adınız<br />
                                <div class="column-1">
                                    <asp:TextBox ID="txtAd" runat="server" TabIndex="5"></asp:TextBox>
                                </div>
                            </div>
                            <div class="column-1">
                                E-mail
                        <br />
                                <div class="column-1">
                                    <asp:TextBox ID="txtmail" runat="server" TabIndex="6"></asp:TextBox>
                                </div>
                            </div>
                            <div class="column-1">
                                Mesaj<br />
                                <div class="column-1">
                                    <asp:TextBox ID="txtMesaj" runat="server" TextMode="MultiLine" Height="150px" TabIndex="7"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div style="margin-left: 50px">
                                <asp:Button ID="btnIlet" runat="server" Text="Gönder" OnClick="btnIlet_Click" TabIndex="8" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnIlet" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div id="AnasayfaRight">
            <img src="/content/images/pano/pano.png" />
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>
