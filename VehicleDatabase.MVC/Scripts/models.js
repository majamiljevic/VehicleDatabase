//add
$('body').on('click', '.add-model', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#add-vehicle-model');
    $(this).attr('data-toggle', 'modal');

    $.ajax({
        url: '/Models/AddVehicleModel',
        dataType: "html",
        success: function (data) {
            $('#add-vehicle-model').html(data);
        }
    });
});

$('body').on('click', '.modal-close-btn', function () {
    $('#add-vehicle-model').modal('hide');
});

$('#add-vehicle-model').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});


//edit
$('body').on('click', '.edit-model', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#add-vehicle-model');
    $(this).attr('data-toggle', 'modal');

    var vehicleModelId = $(this).data('model-id');
    $.ajax({
        url: '/Models/EditVehicleModel',
        data: { vehicleModelId: vehicleModelId },
        dataType: "html",
        success: function (data) {
            $('#add-vehicle-model').html(data);
        }
    });
});

$('body').on('click', '.modal-close-btn', function () {
    $('#add-vehicle-model').modal('hide');
});

$('#add-vehicle-model').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});


//delete
$('body').on('click', '.delete-model', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#delete-vehicle-model-modal');
    $(this).attr('data-toggle', 'modal');

    var vehicleModelId = $(this).data('model-id');
    $.ajax({
        url: '/Models/DeleteVehicleModel',
        data: { vehicleModelId: vehicleModelId },
        dataType: "html",
        success: function (data) {
            $('#delete-vehicle-model-modal').html(data);
        }
    });
});

$('body').on('click', '.modal-close-btn', function () {
    $('#delete-vehicle-model-modal').modal('hide');
});

$('#delete-vehicle-model-modal').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});

$("body").on("click", ".delete-vehicle-model-confirmed", function () {
    var vehicleModelId = $('.delete-vehicle-model-id').val();
    $.ajax({
        url: '/Models/DeleteVehicleModelConfirmed',
        data: { vehicleModelId: vehicleModelId },
        dataType: "html",
        success: function (data) {
            $('#delete-vehicle-model-modal').html(data);
            if ($("#form-state").val() == "true") {
                location.reload();
            }
        }
    });
});


