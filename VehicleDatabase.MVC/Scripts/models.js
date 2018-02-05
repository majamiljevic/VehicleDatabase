//add
$('body').on('click', '.add-model', function (e) {
    e.preventDefault();
    $(this).attr('data-target', '#add-vehicle-model');
    $(this).attr('data-toggle', 'modal');

    $.ajax({
        url: '/Models/AddVehicleModelAsync',
        dataType: "html",
        success: function (data) {
            $('#add-vehicle-model').html(data);

            modalAutocomplete();

            $("#SelectedMakeName").on('change onkeypress', function () {
                if ($("#SelectedMakeName").val() == '' || $("#SelectedMakeName").val() == null) {
                    $("#SelectedMakeId").val('');
                }
            });
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
        url: '/Models/EditVehicleModelAsync',
        data: { vehicleModelId: vehicleModelId },
        dataType: "html",
        success: function (data) {
            $('#add-vehicle-model').html(data);

            modalAutocomplete();

            $("#SelectedMakeName").on('change', function () {

                if ($("#SelectedMakeName").val() == '' || $("#SelectedMakeName").val() == null) {
                    $("#SelectedMakeId").val('');
                }

            });
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
        url: '/Models/DeleteVehicleModelAsync',
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
        url: '/Models/DeleteVehicleModelConfirmedAsync',
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

//autocomplete
$("#MakeName").typeahead({
    source: function (query, process) {
        var makes = [];
        map = {};

        // This is going to make an HTTP post request to the controller

        return $.get('/Models/GetFilteredMakesAsync', { query: query }, function (data) {

            // Loop through and push to the array
            $.each(data, function (i, make) {
                map[make.Name] = make;
                makes.push(make.Name);
            });

            // Process the details
            process(makes);
        });
    },
    updater: function (item) {

        var selectedMake = map[item].Id;
        $("#MakeId").val(selectedMake);
        // Set the text to our selected id

        return item;
    }
});

$("#MakeName").on('change', function () {

    if ($("#MakeName").val() == '' || $("#MakeName").val() == null) {
        $("#MakeId").val('');
    }

});
/*
//add autocomplete
$("#Makes").typeahead({
    source: function (query, process) {
        var makes = [];
        map = {};

        // This is going to make an HTTP post request to the controller

        return $.get('/Models/GetFilteredMakesAsync', { query: query }, function (data) {

            // Loop through and push to the array
            $.each(data, function (i, make) {
                map[make.Name] = make;
                makes.push(make.Name);
            });

            // Process the details
            process(makes);
        });
    },
    updater: function (item) {
        debugger;
        var selectedMake = map[item].Id;
        $("#MakeId").val(selectedMake);
        // Set the text to our selected id

        return item;
    }
});

$("#Makes").on('change', function () {
    debugger;
    if ($("#Makes").val() == '' || $("#Makes").val() == null) {
        $("#MakeId").val('');
    }

});*/

function modalAutocomplete() {
    $("#SelectedMakeName").typeahead({
        source: function (query, process) {
            var makes = [];
            map = {};

            // This is going to make an HTTP post request to the controller

            return $.get('/Models/GetFilteredMakesAsync', { query: query }, function (data) {

                // Loop through and push to the array
                $.each(data, function (i, make) {
                    map[make.Name] = make;
                    makes.push(make.Name);
                });

                // Process the details
                process(makes);
            });
        },
        updater: function (item) {

            var selectedMake = map[item].Id;
            $("#SelectedMakeId").val(selectedMake);
            // Set the text to our selected id

            return item;
        }
    });

    /* $("#SelectedMakeName").on('change', function () {
         debugger;
         if ($("#SelectedMakeName").val() == '' || $("#SelectedMakeName").val() == null) {
             $("#SelectedMakeId").val('');
         }
 
     });*/
}