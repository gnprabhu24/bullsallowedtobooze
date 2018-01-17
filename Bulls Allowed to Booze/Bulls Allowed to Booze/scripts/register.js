
    $(function () {
        $("#txtDateofBirth").datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: "-50:+0"
        }).attr('readonly', 'readonly');
    });

    $('#fileImageUpload').bind("change", function (e) {
        var files = e.target.files;
        var file = files[0];
        var reader = new FileReader();

        if (file) {
            readAndConvert(file);
        }

    });
    var imageCropper;
    var webcam_available;

    function readAndConvert(file) {
        if (/\.(jpe?g|png|gif)$/i.test(file.name)) {
            var reader = new FileReader();
            var base64_image;
            reader.addEventListener("load", function () {
                base64_image = this.result;
                document.getElementById('cropImage').src = base64_image;
                $('#fileImageUpload').val('');
                var cropperOpts = {
                    viewport: { width: 200, height: 240 },
                    boundary: { width: 300, height: 300 },
                    showZoomer: true,
                    enableOrientation: true
                };
                imageCropper = $('#cropImage').croppie(cropperOpts);
                cropperModal.dialog("open");
                
            }, false);

            reader.readAsDataURL(file);
        }

    }

    function openImageUpload() {
        document.getElementById('fileImageUpload').click();
        
    }

    function register() {

        hideError();
        var is_validated = validate();

        if (is_validated == true) {

            document.getElementById('btnregister').style.display = "none";
            document.getElementById('btnloader').style.display = "block";

            var uid = document.getElementById('txtUID').value.trim();
            var given_name = document.getElementById('txtGivenName').value.trim();
            var last_name = document.getElementById('txtLastName').value.trim();
            var date_of_birth = document.getElementById('txtDateofBirth').value.trim();
            var uid_image = document.getElementById('uid_image').src.trim();
            var email = document.getElementById('txtEmail').value.trim();

            var UIDdetails = {
                uid: uid,
                given_name: given_name,
                last_name: last_name,
                date_of_birth: date_of_birth,
                uid_verified: false,
                uid_image: uid_image,
                email: email
            };

            var UIDdetails_json = JSON.stringify(UIDdetails);
            

            PageMethods.sendData(UIDdetails_json, function (data) {
                console.log("Success");
                statusModal.dialog("open");
                if (data == true) {
                    document.getElementById('txtStatusSuccess').style.display = "block";
                }
                else {
                    document.getElementById('txtStatusFail').style.display = "block";
                }
                document.getElementById('btnregister').style.display = "block";
                document.getElementById('btnloader').style.display = "none";
                
            },
            function () {
                console.log("Error");
                statusModal.dialog("open");
                document.getElementById('txtStatusFail').style.display = "block";
                document.getElementById('btnregister').style.display = "block";
                document.getElementById('btnloader').style.display = "none";
            });
        }
        //$.ajax({ 
        //    type: "POST", 
        //    url: "http://localhost:64289/Service1.svc/GetData",
        //    data: JSON.stringify({ value: num }), // passing the parameter 
        //    contentType: "application/json; charset=utf-8", 
        //    dataType: "json",
        //    success: function(retValue) {
        //        // Do something with the return value from.Net method
        //        console.log("Success");
        //        console.log(retValue);
        //    },
        //    error: function (xhr) {
        //        console.log("Error");
        //        console.log(xhr);
        //    }
        //}); 
    }
    
    var statusModal = $("#statusModal").dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        width: 500,
        buttons: [{
            text: "OK",
            click: function () {
                statusModal.dialog("close");
            }
        }],
        close: function (event, ui) {
            window.location.reload(true);
        },
        show: { effect: "slide", direction: "down" },
        hide: { effect: "slide", direction: "down" },
        dialogClass: "dialog-class"

    });

    function ValidateEmail(mail) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
            return (true)
        }
        return (false)
    }
    
    function hideError() {
        document.getElementById('err_txtUID').style.display = "none";
        document.getElementById('err_txtGivenName').style.display = "none";
        document.getElementById('err_txtLastName').style.display = "none";
        document.getElementById('err_txtEmail').style.display = "none";
        document.getElementById('err_txtEmailValid').style.display = "none";
        document.getElementById('err_txtDateofBirth').style.display = "none";
        document.getElementById('err_uid_image').style.display = "none";
    }

    function validate() {
        var uid = document.getElementById('txtUID').value.trim();
        var given_name = document.getElementById('txtGivenName').value.trim();
        var last_name = document.getElementById('txtLastName').value.trim();
        var date_of_birth = document.getElementById('txtDateofBirth').value.trim();
        var uid_image = document.getElementById('uid_image').src.trim();
        var email = document.getElementById('txtEmail').value.trim();


        if (uid == "") {
            document.getElementById('err_txtUID').style.display = "block";
            return false;
        }
        if (given_name == "") {
            document.getElementById('err_txtGivenName').style.display = "block";
            return false;
        }
        if (last_name == "") {
            document.getElementById('err_txtLastName').style.display = "block";
            return false;
        }
        if (email == "") {
            document.getElementById('err_txtEmail').style.display = "block";
            return false;
        }
        if (ValidateEmail(email) == false) {
            document.getElementById('err_txtEmailValid').style.display = "block";
            return false;
        }
        if (date_of_birth == "") {
            document.getElementById('err_txtDateofBirth').style.display = "block";
            return false;
        }
        if (uid_image == "" || uid_image == "http://localhost:55880/images/camera-logo.png") {
            document.getElementById('err_uid_image').style.display = "block";
            return false;
        }

        return true;
    }
    try{
        Webcam.set( {
            width: 320,
            height: 240,
            crop_width: 200,
            crop_height: 240,
            image_format: 'jpeg',
            jpeg_quality: 90
        });
        webcam_available = true;
    }
    catch(e){
        webcam_available = false;
    }
    

    function captureImage() {
        Webcam.snap(function (data_uri) {
            document.getElementById('uid_image').src = data_uri;
           
            Webcam.reset();
            webcamModal.dialog("close");
        });
    }

    

    var selectModal = $("#selectModal").dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        width: 500,
        show: { effect: "slide", direction: "down" },
        hide: { effect: "slide", direction: "down" },
        dialogClass: "dialog-class"
    });

    var webcamModal = $("#webcamModal").dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        width: 500,
        show: { effect: "slide", direction: "down" },
        hide: { effect: "slide", direction: "down" },
        buttons: [{
            text: "Capture",
            click: function () {
                captureImage();
            }
        }],
        open: function (event, ui){

        },
        close: function (event, ui) {
            Webcam.reset();
        },
        dialogClass: "dialog-class"
    });

    var cropperModal = $("#cropperModal").dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        width: 700,
        buttons: [
               {
                   text: "Done",
                   click: function () {
                       loadCroppedImage();
                   }

               }],
        close: function (event, ui) {
            imageCropper.croppie('destroy');
        },
        show: { effect: "slide", direction: "down" },
        hide: { effect: "slide", direction: "down" },
        dialogClass: "dialog-class"

    });
        
    function selectImageUploadType(){
        if(webcam_available == true) {
            selectModal.dialog({
                    buttons: [
                   {
                       text: "Take Photo",
                       click: function () {

                           selectModal.dialog("close");
                           webcamModal.dialog("open");
                           Webcam.attach('#my_webcam');
                        }

                    },
                   {
                       text: "Choose Image",
                       click: function () {
                           openImageUpload();
                           selectModal.dialog("close");
                       }

                   }]
            });
            selectModal.dialog("open");
        }
        else {
            selectModal.dialog({
                buttons: [
               {
                   text: "Choose Image",
                   click: function () {
                       openImageUpload();
                       selectModal.dialog("close");
                   }

               }]
            });
            selectModal.dialog("open");
        }

    }

    function loadCroppedImage() {
        imageCropper.croppie('result', {
            type: 'base64',
            size: 'viewport',
            quality: .9,
            format: 'jpeg'
        }).then(function (result) {
            cropperModal.dialog("close");
            document.getElementById('uid_image').src = result;
        });
    }
       
        
