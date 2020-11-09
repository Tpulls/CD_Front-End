$(function () {
    $("#dropArea").filedrop({
        url: window.location.origin + "/CD/UploadFiles",
        allowdfiletypes: ["image/jpeg", "image/png", "image/gif"],
        allowedfileextensions: [".jpg", ".jpeg", ".png", ".gif", ".JPG", ".JPEG", ".PNG", ".GIF"],
        paramname: "files",
        maxfile: 1,
        maxfilesize: 5, // MB
        dragOver: function () {
            $("#dropArea").addClass("active-drop");
        },
        dragLeave: function () {
            $("#dropArea").removeClass("active-drop");
        },
        drop: function () {
            $("#dropArea").removeClass("active-drop");
        },
        afterAll: function (e) {
            $("#dropArea").html("File uploaded sucessfully.");
        },
        uploadFinished: function (i, file, response, time) {
            $("#cdFileName").val(file.name);
        }
    });
});
