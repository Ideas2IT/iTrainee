$(function () {
    var GetUser = function (id) {
        var UserPath = "/Admin/Home/SaveUser?Id=" + id;
        $("#myModalBodyDiv1").load(UserPath, function () {
            $("#myModal1").modal("show");
        });
    };

    $('#delete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)
        var id = button.data('id-to-delete')

        $('#delete-yes-button').on('click', function () {
            $.ajax({
                url: "/Admin/Home/DeleteUser/" + id,
                type: "DELETE",
                success: function () {
                    $("#myModal1").modal("hide");
                },
                error: function () {
                    alert("Unable to delete data");
                },
            })
        })
    })

    $("#btnSubmit").click(function () {
        var a = $("#myForm").serialize();
        debugger;

        $.ajax({
            type: "POST",
            url: "/Admin/Home/SaveUser",
            data: a,
            success: function () {
                $("#myModal1").modal("hide");
            },
            error: function () {
                alert("Unable to save/update data");
            },
        });
    });

    $("#Admin").click(function () {
        debugger
        if ($("#Admin").is(':checked') || $("#Mentor").is(':checked')) {
            $('#Trainee').attr('disabled', true);
        } else {
            $('#Trainee').attr('disabled', false);
        }
    });

    $("#Mentor").click(function () {
        if ($("#Mentor").is(':checked') || $("#Admin").is(':checked')) {
            $('#Trainee').attr('disabled', true);
        } else {
            $('#Trainee').attr('disabled', false);
        }
    });

    $("#Trainee").click(function () {
        if ($("#Trainee").is(':checked')) {
            $('#Admin').attr('disabled', true);
            $('#Mentor').attr('disabled', true);

        } else {
            $('#Admin').attr('disabled', false);
            $('#Mentor').attr('disabled', false);
        }
    });
});