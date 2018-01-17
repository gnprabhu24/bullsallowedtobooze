    

    var confirmModal = $("#confirmModal").dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        width: 500

    });

    function confirmVerifyUID(is_verified) {

        document.getElementById('err_txtComment').style.display = "none";

        confirmModal.dialog({
            buttons: [
           {
               text: "Ok",
               click: function () {
                   verifyUID(is_verified);
               }

           },
           {
               text: "Cancel",
               click: function () {
                   $(this).dialog("close");
               }

           }]
        });

        var comment = document.getElementById('txtComment').value.trim();

        if (is_verified == true) {
            document.getElementById('txtConfirm').innerHTML = "Are you sure the UID Details are Valid?";
            confirmModal.dialog("open");
        }
       else {
            if (comment != "") {
                document.getElementById('txtConfirm').innerHTML = "Are you sure the UID Details are Not Valid?";
                confirmModal.dialog("open");           
            }
            else {
                document.getElementById('err_txtComment').style.display = "block";
            }
        }
        
    }

    function verifyUID(is_verified) {
        var uid = document.getElementById('ContentPlaceHolder1_txtUID').value.trim();
        if (is_verified == true) {
            var comment = "";
            console.log(uid, is_verified, comment);
            PageMethods.verifyUID(uid, is_verified, comment, function (data) {
                console.log("success");
                console.log(data);
                confirmModal.dialog("close");
                windowClose();
            },
            function () {
                console.log("error");
                confirmModal.dialog("close");
                windowClose();
            });
        }
        else {
            var comment = document.getElementById('txtComment').value.trim();

            PageMethods.verifyUID(uid, is_verified, comment, function (data) {
                console.log("success");
                console.log(data);
                confirmModal.dialog("close");
                windowClose();
            },
            function () {
                console.log("error");
                confirmModal.dialog("close");
                windowClose();
            });
        }
        
    }

    function windowClose() {
        window.open('', '_parent', '');
        window.close();
    }
