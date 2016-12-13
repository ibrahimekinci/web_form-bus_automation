<%@ Page Title="" Language="C#" MasterPageFile="/master/site.Master" AutoEventWireup="true" CodeBehind="UyeOl.aspx.cs" Inherits="OtobusBiletSatis.UyeOl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div id="content2">

      <h1>Üyelik Formu</h1>
    <div id="IKortala">
     <div class="column-1">TC Kimlik Numarası 
       <div class="column-1"><asp:TextBox ID="txtTCKNO" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Boş Bırakmayınız" Text="*"  ForeColor="Red" ControlToValidate="txtAd"></asp:RequiredFieldValidator>
       </div>
    </div>
        <div class="column-1">Ad 
       <div class="column-1"><asp:TextBox ID="txtAd" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtAd"></asp:RequiredFieldValidator>
       </div>
    </div>
        <div class="column-1">Soyad 
       <div class="column-1"><asp:TextBox ID="txtSoyad" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtSoyad"></asp:RequiredFieldValidator>
       </div>
    </div>
        <div class="column-1">Cinsiyet 
       <div><asp:DropDownList ID="ddlCinsiyet" runat="server">
           <asp:ListItem>Bay</asp:ListItem>
           <asp:ListItem>Bayan</asp:ListItem>
            </asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="ddlCinsiyet"></asp:RequiredFieldValidator>
       </div>
    </div>
        <div class="column-1">Doğum Tarihi
       <div class="column-1"><asp:TextBox ID="txtDTarih" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtDTarih"></asp:RequiredFieldValidator>
       </div>
    </div>
        <div class="column-1">Telefon
       <div class="column-1"><asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtTel"></asp:RequiredFieldValidator>
           <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Yanlış Format" ControlToValidate="txtTel" ForeColor="Red" ValidationExpression="@&quot;^(0(\d{3}) (\d{3}) (\d{2}) (\d{2}))$&quot;">!!</asp:RegularExpressionValidator>--%>
       </div>
    </div>
        <div class="column-1">E-posta 
       <div class="column-1"><asp:TextBox ID="txtPosta" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtPosta"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Yanlış Format" ControlToValidate="txtPosta" ForeColor="Red" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$">!!</asp:RegularExpressionValidator>
       </div>
    </div>
        <div class="column-1">İl
            <div>
                <asp:DropDownList ID="ddlIl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIl_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="ddlIl"></asp:RequiredFieldValidator>
            </div>
            </div>
            <div class="column-1">İlçe
            <div>
                <asp:DropDownList ID="ddlIlce" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIlce_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="ddlIlce"></asp:RequiredFieldValidator>
            </div>
                </div>
                <div class="column-1">Mahalle
            <div>
                <asp:DropDownList ID="ddlMah" runat="server" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="ddlMah"></asp:RequiredFieldValidator>
            </div>
                    </div>
        <div class="column-1">Kullanıcı Adı
       <div class="column-1"><asp:TextBox ID="txtUser" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtUser"></asp:RequiredFieldValidator></div>
    </div>
        <div class="column-1">Sifre
       <div class="column-1"><asp:TextBox ID="txtPass1" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtPass1"></asp:RequiredFieldValidator>
       </div>
    </div>
        <div class="column-1">Sifre Tekrar
       <div class="column-1"><div class="column-1"><asp:TextBox ID="txtPass2" runat="server" TextMode="Password"></asp:TextBox> <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Boş bırakmayınız" Text="*" ForeColor="Red" ControlToValidate="txtPass2"></asp:RequiredFieldValidator>
           <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Şifrenizi kontorl ediniz" Text="!" ForeColor="Red" ControlToValidate="txtPass2" ValueToCompare="txtPass1" ControlToCompare="txtPass1"></asp:CompareValidator>
      </div>
    </div>
        <div class="column-1">
            <center>
            <div class="ortala"><asp:Button ID="btnKaydol" CssClass="buton" runat="server" Text="Kaydol" OnClick="btnKaydol_Click" /></div>
            </center>
        </div>    
      </div>
         <div class="clear"></div>
  </div>
            <div class="clear"></div>
               </div>
</asp:Content>
