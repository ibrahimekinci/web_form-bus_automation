<%@ Page Title="" Language="C#" MasterPageFile="/master/site.Master" AutoEventWireup="true" CodeBehind="InsanKaynaklari.aspx.cs" Inherits="OtobusBiletSatis.InsanKaynaklari" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>İnsan Kaynakları</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content2">
         <h1>İş Başvuru Formu</h1>
       <div id="IKortala">
            <div class="column-1">Ad Soyad
            <div class="column-1"><asp:TextBox ID="txtAd" runat="server"></asp:TextBox>&nbsp
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Lütfen Adınızı Giriniz" Text="*" ForeColor="Red" ControlToValidate="txtAd"></asp:RequiredFieldValidator>
            </div>  </div>          
            <div class="column-1">Doğum Yeri
            <div class="column-1"><asp:TextBox ID="txtDYeri" runat="server"></asp:TextBox>&nbsp
            </div></div>
            <div class="column-1">Doğum Tarihi
            <div class="column-1"><asp:TextBox ID="txtTarih" runat="server"></asp:TextBox>&nbsp
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Doğum Tarihini Giriniz" Text="*" ForeColor="Red" ControlToValidate="txtTarih"></asp:RequiredFieldValidator>
            </div> </div>

            <div class="column-1">Medeni Hal
          <div class="column-1"> &nbsp<asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>Bekar</asp:ListItem>
                <asp:ListItem>Evli</asp:ListItem>
                </asp:DropDownList>
            </div></div>
            <div class="column-1">GSM
            <div class="column-1"><asp:TextBox ID="txtGsm" runat="server"></asp:TextBox>&nbsp
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Telefon Bilgisi Giriniz" Text="*" ForeColor="Red" ControlToValidate="txtGsm"></asp:RequiredFieldValidator>
            </div></div>
            <div class="column-1">Mail
            <div class="column-1"><asp:TextBox ID="txtPosta" runat="server"></asp:TextBox>&nbsp
            </div></div>
            <div class="column-1">İkamet Adresi
            <div class="column-1"><asp:TextBox ID="txtAdres" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="İkamet Adresini Giriniz" Text="*" ForeColor="Red" ControlToValidate="txtAdres"></asp:RequiredFieldValidator>
            </div></div>
            <div class="column-1">Kısa Bilgi
            <div class="column-1"><asp:TextBox ID="txtBilgi" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Kısa Bilgi zorunludur" Text="*" ForeColor="Red" ControlToValidate="txtBilgi"></asp:RequiredFieldValidator>
            </div></div>

            <div class="column-1">CV</div>
            <div><asp:FileUpload ID="FileUpload1" runat="server" /></div>
            <br />
            <div class="buton">
            <asp:Button ID="btnGonder" runat="server" Text="Gönder" CssClass="buton" OnClick="btnGonder_Click" /></div>               
         </div>
        <div class="errors">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
        <div class="IKresim imgYuvarlaBorder">
                        <img src="/content/images/site/ik3.jpg" width="500" class="imgYuvarlaBorder"/>
        </div>
<div class="clear"></div>       
    </div>
</asp:Content>
