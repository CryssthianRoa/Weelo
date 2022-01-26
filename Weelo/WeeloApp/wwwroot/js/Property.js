$(document).ready(function () {
    LoadProperties();
    CRUDGrid();

    $("#inpSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tblContent tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    })
})
var selectO;
function LoadProperties() {
    $("#tblHeaders").empty();
    $("#tblContent").empty();

    $.ajax({
        type: "GET",
        url: "/Home/GetProperties",
        async: false,
        success: function (result) {
            var resultJSON = JSON.parse(result);
            
            for (var i = 0; i < Object.keys(resultJSON.Value[0]).length; i++) {
                var header = Object.keys(resultJSON.Value[0])[i];

                if (header != "IdProperty" &&
                    header != "IdOwnerNavigation" &&
                    header != "Audits" &&
                    header != "PropertyImages" &&
                    header != "PropertyTraces") {
                    $("#tblHeaders").append("<th>" + header + "</th>");
                }
            }
            $("#tblHeaders").append("<th>Photo</th><th>Actions</th>");

            for (var i = 0; i < resultJSON.Value.length; i++) {
                $("#tblContent").append("<tr>" +
                    "<td id='IdProperty' style='display:none'>" + resultJSON.Value[i].IdProperty + "</td>" +
                    "<td id='IdOwner' class='" + resultJSON.Value[i].IdOwner + "'>" + resultJSON.Value[i].IdOwnerNavigation.Name + "</td>" +
                    "<td id='Name'>" + resultJSON.Value[i].Name + "</td>" +
                    "<td id='Address'>" + resultJSON.Value[i].Address + "</td>" +
                    "<td id='Price'>" + resultJSON.Value[i].Price + "</td>" +
                    "<td id='CodeInternal'>" + resultJSON.Value[i].CodeInternal + "</td>" +
                    "<td id='Year'>" + resultJSON.Value[i].Year + "</td>" +
                    "<td id='Photo'><a rel='popover-" + resultJSON.Value[i].IdProperty + "' onClick='Photos(" + resultJSON.Value[i].IdProperty + ")'><i class='material-icons'>&#xe3c4;</i><input type='file' class='form-control form-control-sm setPhoto' onChange='LoadPhoto(event, " + resultJSON.Value[i].IdProperty + ")'></a></td>" +
                    "<td>" +
                        "<a class='add' title='Add' data-toggle='tooltip'><i class='material-icons'>&#xE03B;</i></a>" +
                        "<a class='edit' title='Edit' data-toggle='tooltip'><i class='material-icons'>&#xE254;</i></a>" +
                        "<a class='delete' title='Delete' data-toggle='tooltip'><i class='material-icons'>&#xE872;</i></a>" +
                    "</tr>");
                $("table tbody tr").eq($("table tbody tr:last-child").index()).find(".add").toggle();
            };
        },
        error: function (result) {
            alert("Error JS");
        }
    });
    
}

function CRUDGrid() {
    var dataProperties = {
        IdOwner: 0,
        Name: "",
        Address: "",
        Price: 0,
        CodeInternal: "",
        Year: 0
    }
    
    $('[data-toggle="tooltip"]').tooltip();
    var actions;

    $(".add-new").click(function () {
        $(this).attr("disabled", "disabled");
        var index = $("table tbody tr:last-child").index();
        actions = $("table td:last-child").html();
        GetOwners(this, "add");
        var row = '<tr>' +
            '<td>' + selectO + '</td>' +
            '<td><input type="text" class="form-control" name="Name" id="Name"></td>' +
            '<td><input type="text" class="form-control" name="Address" id="Address"></td>' +
            '<td><input type="text" class="form-control" name="Price" id="Price"></td>' +
            '<td><input type="text" class="form-control" name="CodeInternal" id="CodeInternal"></td>' +
            '<td><input type="text" class="form-control" name="Year" id="Year"></td>' +
            '<td>' + actions + '</td>' +
            '</tr>';
        $("table").append(row);
        $("table tbody tr").eq(index + 1).find(".add, .edit").toggle();
        $('[data-toggle="tooltip"]').tooltip();
    });

    $(document).on("click", ".add", function () {
        var empty = false;
        var input = $(this).parents("tr").find('input[type="text"], select');
        input.each(function () {
            if (!$(this).val() && this.id == "IdProperty") {
                $(this).addClass("error");
                empty = true;
            } else {
                $(this).removeClass("error");
            }
        });
        $(this).parents("tr").find(".error").first().focus();
        if (!empty) {
            input.each(function () {
                dataProperties[this.id] = $(this).val();
                if (this.id != "Photo") {
                    $(this).parent("td").html((this.id != "IdOwner" ? $(this).val() : $($(this).find('option[value="' + $(this).val() + '"]')[0]).text()));
                }
            });
            var cells = this;
            $.ajax({
                type: "POST",
                url: '/Home/SetProperties',
                data: dataProperties,
                success: function (result) {
                    result = JSON.parse(result);
                    $(cells).parents("tr").find(".add, .edit").toggle();
                    $(".add-new").removeAttr("disabled");

                },
                error: function (xhr) {
                    var xhr2 = JSON.parse(xhr.responseText);
                    var err = "Error: " + xhr2.message + " - Detalle: " + xhr2.value + " - Codigo Error: " + xhr.status;
                    TimerStop();
                    alert(err);
                }
            });
        }
    });
    
    $(document).on("click", ".edit", function () {
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            if (this.id == "IdOwner") {
                GetOwners(this, "edit");
            } else if (this.id == "Photo") {
            }
            else {
                $(this).html('<input type="text" id="' + this.id + '" class="form-control" value="' + $(this).text() + '"> ');
            }
            
        });
        $(this).parents("tr").find(".edit, .add").toggle();
        $(".add-new").attr("disabled", "disabled");
    });
    
    $(document).on("click", ".delete", function () {
        var deleteArg = this;
        $.ajax({
            type: "DELETE",
            url: '/Home/DeleteProperties',
            data: { idProperty: $($(deleteArg).parents("tr").find("#IdProperty")[0]).text() },
            success: function (result) {
                result = JSON.parse(result);
                $(deleteArg).parents("tr").remove();
                $(".add-new").removeAttr("disabled");

            },
            error: function (xhr) {
                var xhr2 = JSON.parse(xhr.responseText);
                var err = "Error: " + xhr2.message + " - Detalle: " + xhr2.value + " - Codigo Error: " + xhr.status;
                TimerStop();
                alert(err);
            }
        });
        
    });
}

function GetOwners(e, type) {
    $.ajax({
        type: "GET",
        url: '/Home/GetOwners',
        async: false,
        success: function (result) {
            result = JSON.parse(result);
            var html = '<select id="' + (e.id != "" ? e.id : "IdOwner") +'" class="form-control form-select" aria-label="Default select example">';
            
            for (var i = 0; i < result.Value.length; i++) {
                if (result.Value[i].IdOwner == e.className) {
                    html += '<option selected value="' + e.className + '">' + $(e).text() + '</option>';
                } else {
                    html += '<option value="' + result.Value[i].IdOwner + '">' + result.Value[i].Name + '</option>';
                }
            }
            if (type == "edit") {
                $(e).html(html + '</select>');
            } else {
                selectO = html + '</select>';
            }
            
        },
        error: function (xhr) {
            var xhr2 = JSON.parse(xhr.responseText);
            var err = "Error: " + xhr2.message + " - Detalle: " + xhr2.value + " - Codigo Error: " + xhr.status;
            TimerStop();
            alert(err);
        }
    });
}

function Photos(id) {
    $.ajax({
        type: "POST",
        url: '/Home/GetImages',
        data: { idProperty: id },
        success: function (result) {
            result = JSON.parse(result);
            $('a[rel=popover-' + id + ']').popover({
                html: true,
                trigger: 'click',
                placement: 'left',
                content: function () {
                    var images = "";
                    for (var i = 0; i < result.Value.length; i++) {
                        if (!result.Value[i].Enabled) {
                            continue;
                        }
                        images += "<img src='../images/" + result.Value[i].File + "' class='photoHover' >";
                    }
                    return images;
                }
            });

        },
        error: function (xhr) {
            var xhr2 = JSON.parse(xhr.responseText);
            var err = "Error: " + xhr2.message + " - Detalle: " + xhr2.value + " - Codigo Error: " + xhr.status;
            TimerStop();
            alert(err);
        }
    });

    
}

function LoadPhoto(e, id) {
    var files = e.target.files;
    //var myID = 3; //uncomment this to make sure the ajax URL works
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }

            $.ajax({
                type: "POST",
                url: '/Home/SetPhoto',
                contentType: false,
                processData: false,
                data: data,
                async:false,
                success: function (result) {
                    result = JSON.parse(result);
                    $.ajax({
                        type: "POST",
                        url: '/Home/SetDbPhoto',
                        data: { file: files[0].name, idProperty: id },
                        success: function (result) {
                            result = JSON.parse(result);
                            LoadProperties();
                        }
                    });
                }
            });
        } else {
            alert("This browser doesn't support HTML5 file uploads!");
        }
    }
};