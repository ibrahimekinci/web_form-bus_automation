<%@ Page Title="" Language="C#" MasterPageFile="/master/menulu.master" AutoEventWireup="true" CodeBehind="BiletIslemleri.aspx.cs" Inherits="OtobusBiletSatis.BiletIslemleri" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headAlt" runat="server">
    <link href="/content/plugin/datepicker/jquery-ui-datepicker.css" rel="stylesheet" />
    <script src="/content/plugin/datepicker/jquery-ui-datepicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="bilet" runat="server">
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
            <input type="hidden" id="txtCinsiyet" value="<%= cinsiyet %>">
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btnBitir" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="Click" />
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
                    <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
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
                            <asp:CheckBox ID="cbHatirla" runat="server" Text="Beni Hatırla" ForeColor="Black" TabIndex="2" />
                            <a href="/login/SifremiUnuttum.aspx" target="_blank">Şifremi Unuttum</a>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGirisYap" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btnBitir" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="column-1">
        <div class="column-1">
            <div class="solaYasla">
                <div class="AnaSayfaBaslik">Nereden</div>
                <asp:DropDownList ID="ddlGidis" runat="server"></asp:DropDownList>
            </div>
            <div class="solaYasla margin-right-30">
                <div class="AnaSayfaBaslik">Nereye</div>
                <asp:DropDownList ID="ddlDonus" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div class="column-1">
        <div style="width: 40%">
            <script>
                $(function () {
                    $("#txtGidisTarihi").datepicker({
                        showOn: "button",
                        buttonImage: "/content/images/sablon/calender.gif",
                        buttonImageOnly: true,
                        buttonText: "Select date"

                    });
                });
            </script>
            Gidiş Tarihi:
            <input type="text" id="txtGidisTarihi" name="txtGidisTarihi" value="<%= tarih %>">
        </div>
    </div>
    <div class="column-1" id="divDonusTarihSec">
        <div style="width: 40%">
            <script>
                $(function () {
                    $("#txtDonusTarihi").datepicker({
                        showOn: "button",
                        buttonImage: "/content/images/sablon/calender.gif",
                        buttonImageOnly: true,
                        buttonText: "Select date"
                    });
                });
            </script>
            <p>
                Donus Tarihi:
                <input type="text" id="txtDonusTarihi" name="txtDonusTarihi" value="Tarih Seçiniz">
            </p>
        </div>

    </div>
    <div class="column-1">
        <asp:Button ID="btnSeferAra" runat="server" Text="Sefer Ara" CssClass="seferAra" OnClick="btnSeferAra_Click" />
    </div>

    <div class="column-1">
        <div class="solaYasla margin-right-30">
            <label id="rbTekYon">
                <input type="radio" name="radioYon" id="tekYonRadio" value="Tek Yön" />Tek Yön</label>
            <label id="rbCiftYon">
                <input type="radio" name="radioYon" id="ciftYonRadio" value="Çift Yön" />Gidiş Dönüş</label>
            <input type="hidden" id="txtCiftYon" value="">
        </div>
        <div class="clear"></div>
    </div>


    <div class="column-1">
        <asp:UpdatePanel ID="upGidisBox" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <input type="hidden" id="txtGidisKoltukSayisi" value="<%= gidisKoltukSayisi.ToString() %>">
                <div class="column-1">
                    <asp:DataList ID="dlGidisSeferler" runat="server" OnItemCommand="dlSeferler_ItemCommand">
                        <HeaderTemplate>
                            <div class="solaYasla dlHeader">Gidiş Seferleri</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="seferSatir">
                                <div class="seferSutun">
                                    <asp:Button ID="btnSeferSec" runat="server" Text="Seç" CommandArgument='<%# Eval("seferID") %>' CommandName="gidis" CssClass="btnSeferSecGidis" />
                                </div>
                                <div class="seferSutun"><b>Saat:</b>  <%# Eval("saat") %></div>
                                <div class="seferSutun"><b>Durak Sayısı:</b> <%# Eval("DurakSayisi") %></div>
                                <div class="seferSutun"><b>Mola Sayısı:</b> <%# Eval("MolaSayisi") %></div>
                                <div class="seferSutun"><b>Mesafe(KM):</b> <%# Eval("Mesafe") %></div>
                                <div class="seferSutun"><b>Süre:</b> <%# Eval("Süre") %></div>
                                <div class="seferSutun"><b>İnd.Fiyat(TL): </b><%# Eval("indirimliFiyat") %></div>
                                <div class="seferSutun"><b>Fiyat(TL):</b> <%# Eval("Fiyat") %></div>

                                <div class="clear"></div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div id="divGidisKoltuklar" runat="server">
                    <div class="column-1 gidis">
                        Koltuk No:
                        <input type="text" id="txtGidisNo" name="txtGidisNo" value="Koltuk Seçiniz" disabled="disabled">
                    </div>

                    <div id="divGidisBox" class="column-1 gidis">
                        <div class="yon gidis" id="biletGidis">
                            <div class="koltukBox">
                                <div id="divGidisKoltukSira1" class="koltukSiraUst" runat="server">
                                </div>
                                <div id="divGidisKoltukSira2" class="koltukSiraAlt" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                      <div class="column-1">
                    <asp:RadioButtonList ID="rbBiletIslemTuruGidis" runat="server" CssClass="radio" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="Bilet Satın Al" Value="1"></asp:ListItem>
                        <asp:ListItem  Text="Rezervasyon" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                </div>
              
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="dlGidisSeferler" EventName="ItemCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upDonusBox" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <input type="hidden" id="txtDonusKoltukSayisi" value="<%= donusKoltukSayisi.ToString() %>">
                <asp:DataList ID="dlDonusSeferler" runat="server" OnItemCommand="dlSeferler_ItemCommand">
                    <HeaderTemplate>
                        <div class="solaYasla dlHeader">Dönüş Seferleri</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="seferSatir">
                            <div class="seferSutun">
                                <asp:Button ID="btnSeferSec" runat="server" Text="Seç" CommandArgument='<%# Eval("seferID") %>' CommandName="donus" CssClass="btnSeferSecDonus" />
                            </div>
                            <div class="seferSutun"><b>Saat:</b>  <%# Eval("saat") %></div>
                            <div class="seferSutun"><b>Durak Sayısı:</b> <%# Eval("DurakSayisi") %></div>
                            <div class="seferSutun"><b>Mola Sayısı:</b> <%# Eval("MolaSayisi") %></div>
                            <div class="seferSutun"><b>Mesafe(KM):</b> <%# Eval("Mesafe") %></div>
                            <div class="seferSutun"><b>Süre:</b> <%# Eval("Süre") %></div>
                            <div class="seferSutun"><b>İnd.Fiyat(TL): </b><%# Eval("indirimliFiyat") %></div>
                            <div class="seferSutun"><b>Fiyat(TL):</b> <%# Eval("Fiyat") %></div>

                            <div class="clear"></div>
                        </div>
                    </ItemTemplate>

                </asp:DataList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>

                <div id="divDonusKoltuklar" runat="server">
                    <div id="divDonusBox" class="column-1">
                        <div class="column-1 donus">
                            Koltuk No:
                            <input type="text" id="txtDonusNo" name="txtDonusNo" value="Koltuk Seçiniz" disabled="disabled">
                        </div>
                        <div class="column-1 " id="divDonus">
                            <div class="yon donus" id="biletDonus">
                                <div class="koltukBox">

                                    <div id="divDonusKoltukSira1" class="koltukSiraUst" runat="server">
                                    </div>
                                    <div id="divDonusKoltukSira2" class="koltukSiraAlt" runat="server">
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>

                        <div class="clear"></div>
                    </div>
                          <div class="column-1">

                    <asp:RadioButtonList ID="rbBiletIslemTuruDonus" runat="server" CssClass="radio" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="Bilet Satın Al" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Rezervasyon" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                </div>
          
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeferAra" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="dlDonusSeferler" EventName="ItemCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div class="column-1">
        <asp:UpdatePanel ID="upBitir" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnBitir" runat="server" Text="işlemi Bitir" OnClick="btnBitir_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
