$(function () {
    //var GetUser = function (id) {
    //    var UserPath = "/Admin/Home/SaveUser?Id=" + id;
    //    $("#myModalBodyDiv1").load(UserPath, function () {
    //        $("#myModal1").modal("show");
    //    });
    //};

    $(document).on('click', '.addDatePicker', function () {
        setTimeout(function () {
            $('#ui-datepicker-div').remove();
            $("#datepicker").datepicker({ minDate: "-60Y", maxDate: "-18Y", changeMonth: true, changeYear: true });
        }, 1000)
    })

    $(document).on('click', '.calendar-icon', function () {
        $(this).prev('input').focus();
    })

    $('#delete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)
        var id = button.data('id-to-delete')
        $('#delete-yes-button').on('click', function () {
            $.ajax({
                url: "/Admin/Home/DeleteUser/" + id,
                type: "Delete",
                dataType: "JSON",
                success: function () {
                    $("#myModal1").modal("hide");
                    window.location.reload();
                },
                error: function () {
                    alert("Unable to delete data");
                },
            })
        })
    })

    $("#Admin").click(function () {
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