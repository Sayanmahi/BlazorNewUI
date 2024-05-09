window.ShowToastr = (type, message) => {
    if (type == "success") {
        toastr.success(message, "Operation Successful", { timeOut: 5000 })

    }
    if (type == "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 })

    }
}
window.fitColumnWidth = () => {
    var table = document.querySelector('table');
    if (table) {
        var th = table.querySelector('th');
        if (th) {
            th.style.width = 'auto';
        }
    }
};
window.registerDblClickHandler = () => {
    document.querySelectorAll('th').forEach(th => {
        th.addEventListener('onclick', fitColumnWidth);
    });
};
window.tableResize = {
    makeResizable: function (elementId) {
        $(document).ready(function () {
            $("#" + elementId + " Table").resizable({
                handles: "e"
            });
        });
    }
};