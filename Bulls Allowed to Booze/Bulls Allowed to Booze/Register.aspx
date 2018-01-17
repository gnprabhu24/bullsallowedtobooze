<%@ Page Language="C#" MasterPageFile="~/BullsAllowedtoBooze.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Bulls_Allowed_to_Booze.Register" %>

<asp:Content ID="Registerform" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="form-class">
            <div class="form-class-heading">Provide your Information</div>
            <div>
                <label><span>UID:<span class="required_field">*</span></span>
                <input class="input-field" type="text" id="txtUID" name="txtUID" placeholder="Enter your UID. Eg.: U55555555" /></label>
            </div>
            <div id="err_txtUID" class="err-div">
                <label><span>&nbsp;</span>Please enter Your UID.</label>
            </div>

            <div>
                <label><span>Given Name:<span class="required_field">*</span></span>
                <input class="input-field" type="text" id="txtGivenName" name="txtGivenName" placeholder="Enter your Given Name"/></label>
            </div>
            <div id="err_txtGivenName" class="err-div">
                <label><span>&nbsp;</span>Please enter Your Given Name.</label>
            </div>

            <div>
                <label><span>Last Name:<span class="required_field">*</span></span>
                <input class="input-field" type="text" id="txtLastName" name="txtLastName" placeholder="Enter your Last Name" /></label>
            </div>
             <div id="err_txtLastName" class="err-div">
                <label><span>&nbsp;</span>Please enter Your Last Name.</label>
            </div>

            <div>
                <label><span>Email:<span class="required_field">*</span></span>
                <input class="input-field" type="text" id="txtEmail" name="txtEmail" placeholder="Enter your Email "/></label>
            </div>
            <div id="err_txtEmail" class="err-div">
                <label><span>&nbsp;</span>Please enter Your Email Id.</label>
            </div>
            <div id="err_txtEmailValid" class="err-div">
                <label><span>&nbsp;</span>Please enter a Valid Email Id. Eg: rockybull@mail.usf.edu </label>
            </div>

            <div>
                <label><span>Date of Birth:<span class="required_field">*</span></span>
                <input class="input-field" type="text" id="txtDateofBirth" name="txtDateofBirth" /></label>
            </div>
            <div id="err_txtDateofBirth" class="err-div" >
                <label><span>&nbsp;</span>Please enter Your Date of Birth.</label>
            </div>

            <div>
                <label><span>Photograph:<span class="required_field">*</span></span></label>
                <div >
                    <span>&nbsp;</span>
                    <a  href="javascript:selectImageUploadType();" style="width: 200px;">
                        <img src="images/camera-logo.png" id="uid_image" style="width: 200px;"/>
                    </a>
                    
                </div>
            </div>
            <div id="err_uid_image" class="err-div">
                <label><span>&nbsp;</span>Photograph is required.</label>
            </div>

            <div>
                <label><span>&nbsp;</span></label><button type="button" id="btnregister" onclick="javascript:register();">Register</button>
                <img src="images/loader.gif" id="btnloader" style="display:none; width:auto" />
            </div>
            
    </div>
    </form>
    <input type="file" id="fileImageUpload" name="fileImageUpload" accept="image/*" style="display: none;"/>
    <div id="selectModal" title="Choose">
      <p>Choose Method to Upload Photograph.</p>
    </div>
    <div id="webcamModal" title="Take Photo" >
        <div id="my_webcam" ></div>
    </div>
    <div id="cropperModal" title="Crop image">
      <img id="cropImage" style="max-width: 500px;" />
    </div>
    <div id="statusModal" title="Register Request Status">
      <p id="txtStatusSuccess" style="display:none;">Your Register request is successfully sent. You will recieve an email once your details are verified.</p>
      <p id="txtStatusFail" style="display:none;">Your Register request Failed.</p>
    </div>

</asp:Content>
<asp:Content ID="Registerscript" ContentPlaceHolderID="scripts" runat="server">
    <!--Libraries-->
    <script src="scripts/webcam.js" type="text/javascript"></script>
    <script src="scripts/croppie.js" type="text/javascript"></script>

    <!-- Custom Scripts -->
    <script src="scripts/register.js" type="text/javascript"></script>
</asp:Content>
