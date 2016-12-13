<%@ Page Title="" Language="C#" MasterPageFile="/master/menulu.master" AutoEventWireup="true" CodeBehind="BenimSayfam.aspx.cs" Inherits="OtobusBiletSatis.BenimSayfam" %>

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
            <asp:AsyncPostBackTrigger ControlID="btnGuncelle" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upBenimSayfam" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div class="column-1">

                <div class="column-1 solaYasla">
                    TC:<br />
                    <asp:TextBox ID="txtTC" runat="server" ReadOnly="true" Width="175px"></asp:TextBox>
                </div>

                <div class="column-1 solaYasla">
                    AD:
                    <br />
                    <asp:TextBox ID="txtAd" runat="server" ReadOnly="true" Width="175px"></asp:TextBox>
                </div>


                <div class="column-1 solaYasla">
                    SOYAD:<br />
                    <asp:TextBox ID="txtSoyad" runat="server" ReadOnly="true" Width="175px"></asp:TextBox>
                </div>

                <div class="column-1 solaYasla">
                    TEL:<br />
                    <asp:TextBox ID="txtTel" runat="server" Width="175px"></asp:TextBox>
                </div>
                <div class="column-1 solaYasla">
                    EV TEL:<br />
                    <asp:TextBox ID="txtEvTel" runat="server" Width="175px"></asp:TextBox>
                </div>
                <div class="column-1 solaYasla">
                    MAİL:<br />
                    <asp:TextBox ID="txtMail" runat="server" Width="175px" Enabled="false"></asp:TextBox>
                </div>
                <div class="column-1 solaYasla">
                    SEHİR<br />
                    <asp:DropDownList ID="ddlil" runat="server" Height="16px" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlil_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="column-1 solaYasla">
                    İLCE<br />
                    <asp:DropDownList ID="ddlilce" runat="server" Height="16px" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlilce_SelectedIndexChanged"></asp:DropDownList>
                </div>


                <div class="column-1 solaYasla">
                    MAHALLE<br />
                    <asp:DropDownList ID="ddlmah" runat="server" Height="16px" Width="175px"></asp:DropDownList>
                </div>

                <div class="column-1 solaYasla">
                    ADRES AÇIKLAMA<br />
                    <asp:TextBox ID="txtAdresAciklama" runat="server" TextMode="MultiLine" Height="150"></asp:TextBox>
                </div>
              
                    <div class="column-1 solaYasla">
                        KULLANICI ADI:<br />
                        <asp:TextBox ID="txtKullanici" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="column-1 solaYasla">
                        ESKİ ŞİFRE<br />
                        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="column-1 solaYasla">
                        YENİ ŞİFRE<br />
                        <asp:TextBox ID="txtYeniSifre" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="column-1 solaYasla">
                        YENİ ŞİFRE TEKRAR<br />
                        <asp:TextBox ID="txtYeniSifreTekrar" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
         
                <div class="clear"></div>
                <div class="column-1">&nbsp;</div>
                <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" OnClick="btnGuncelle_Click" />
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
