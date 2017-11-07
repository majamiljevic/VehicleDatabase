//add
$('body').on('click', '.add-manufacturer', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#modal-container');
    $(this).attr('data-toggle', 'modal');

    $.ajax({
        url: '/Manufacturers/AddManufacturer',
        dataType: "html",
        success: function (data) {
            $('#modal-container').html(data);
        }
    });
});

$('body').on('click', '.modal-close-btn', function () {
    $('#modal-container').modal('hide');
});

$('#modal-container').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});




//delete
$('body').on('click', '.delete-manufacturer', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#delete-manufacturer-modal');
    $(this).attr('data-toggle', 'modal');

    var manufacturerId = $(this).data('manufacturer-id');
    $.ajax({
        url: '/Manufacturers/DeleteManufacturer',
        data: { manufacturerId: manufacturerId },
        dataType: "html",
        success: function (data) {
            $('#delete-manufacturer-modal').html(data);
        }
    });
});

$('body').on('click', '.modal-close-btn', function () {
    $('#delete-manufacturer-modal').modal('hide');
});

$('#delete-manufacturer-modal').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});


$("body").on("click", ".delete-manufacturer-confirmed", function () {
    var manufacturerId = $('.delete-manufacturer-id').val();
    $.ajax({
        url: '/Manufacturers/DeleteManufacturerConfirmed',
        data: { manufacturerId: manufacturerId },
        dataType: "html",
        success: function (data) {
            location.reload();
        }
    });
});



//edit
$('body').on('click', '.edit-manufacturer', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#modal-container');
    $(this).attr('data-toggle', 'modal');

    var manufacturerId = $(this).data('manufacturer-id');
    $.ajax({
        url: '/Manufacturers/EditManufacturer',
        data: { manufacturerId: manufacturerId },
        dataType: "html",
        success: function (data) {
            $('#modal-container').html(data);
        }
    });
});

$('body').on('click', '.modal-close-btn', function () {
    $('#modal-container').modal('hide');
});

$('#modal-container').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});

