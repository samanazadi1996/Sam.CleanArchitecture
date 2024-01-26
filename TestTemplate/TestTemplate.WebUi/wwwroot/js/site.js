function ReloadUnobtrusive() {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");
}
ReloadUnobtrusive()

function BaseReloadList(url, tableId) {
    $.get(url + window.location.search, function (data) {
        document.getElementById(tableId).innerHTML = data
        ReloadUnobtrusive()
    })
}
function LoadPageToModal(url, modalId, modalBodyId) {
    $.get(url, function (data) {
        $(`#${modalBodyId}`).html(data)
        $(`#${modalId}`).modal("show")
        ReloadUnobtrusive()
    })
}

function BasePostItem(formId, url, modalId, runScript) {
    if ($(formId).valid()) {
        $.ajax({
            type: "POST",
            url: url,
            data: $(formId).serialize(),
            success: function (data) {
                if (data.success) {
                    $(modalId).modal("hide")
                    $('form').trigger("reset");
                    eval(runScript)
                } else {
                    var messages = [];
                    if (data && data.operationErrors) {
                        for (var i = 0; i < data.operationErrors.length; ++i)
                            messages.push(data.operationErrors[i].description);
                    }
                    alert(messages.join('-'))
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var messages = [];
                if (XMLHttpRequest?.responseJSON?.ValidationErrors) {
                    for (var i = 0; i < XMLHttpRequest.responseJSON.ValidationErrors.length; i++) {
                        messages.push(XMLHttpRequest.responseJSON.ValidationErrors[i]["ErrorMessage"])
                    }
                } else messages.push("Internal Error")

                alert(messages.join('-'))

            }
        });
    }
}
function BaseDeleteItem(url, itemId, runScript) {

    $.ajax({
        type: "POST",
        url: url,
        data: { Id: itemId },
        success: function (data) {
            if (data.success) {
                eval(runScript)
            } else {
                var messages = [];
                if (data && data.operationErrors) {
                    for (var i = 0; i < data.operationErrors.length; ++i)
                        messages.push(data.operationErrors[i].description);
                }
                alert(messages.join('-'))
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var messages = [];
            if (XMLHttpRequest?.responseJSON?.ValidationErrors) {
                for (var i = 0; i < XMLHttpRequest.responseJSON.ValidationErrors.length; i++) {
                    messages.push(XMLHttpRequest.responseJSON.ValidationErrors[i]["ErrorMessage"])
                }
            } else messages.push("Internal Error")

            alert(messages.join('-'))
        }
    });

}