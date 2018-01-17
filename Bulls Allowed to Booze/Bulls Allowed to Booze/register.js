    function register() {
        var num = 12345;
        PageMethods.sendData(JSON.stringify({ value: num }), function (data) {
            console.log("success");
            console.log(data);
        },
        function () {
            console.log("error");
        });
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


    var pos = 0, ctx = null, saveCB, image = [];

    var canvas = document.createElement("canvas");
    canvas.setAttribute('width', 320);
    canvas.setAttribute('height', 240);

    ctx = canvas.getContext("2d");
    image = ctx.getImageData(0, 0, 320, 240);

    saveCB = function (data) {

        var col = data.split(";");
        var img = image;

        for (var i = 0; i < 320; i++) {
            var tmp = parseInt(col[i]);
            img.data[pos + 0] = (tmp >> 16) & 0xff;
            img.data[pos + 1] = (tmp >> 8) & 0xff;
            img.data[pos + 2] = tmp & 0xff;
            img.data[pos + 3] = 0xff;
            pos += 4;
        }

        if (pos >= 4 * 320 * 240) {
            ctx.putImageData(img, 0, 0);
            var base64_image = canvas.toDataURL("image/png");
            document.getElementById('imgClicked').src = base64_image;
            pos = 0;
        }
    };

    $("#webcam").webcam({

        width: 320,
        height: 240,
        mode: "callback",
        swffile: "/bullsallowedtobooze/images/jscam_canvas_only.swf",

        onSave: saveCB,

        onCapture: function () {
            webcam.save();
        },

        debug: function (type, string) {
            console.log(type + ": " + string);
        },

        onLoad: function () {
            console.log(0);
            // Page load
            var cams = webcam.getCameraList();
            for (var i in cams) {
                console.log(cams[i]);
            }
        }
    });

