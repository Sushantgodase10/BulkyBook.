

$(document).ready(function () {
    $('#myTable').DataTable({
        'ajax': '/admin/product/getall',
        'columns': [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'listPrice' },
            { data: 'auther' },
            { data: 'catgories.categoryName' },
            {
                data: 'id',
                'render': function (data) {
                    return `<a href="/admin/product/upsert/${data}" class="btn btn-md btn-primary"><i class="bi bi-pencil-square"></i> Update</a>
                            <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-md btn-danger"><i class="bi bi-trash-fill"></i> Delete</a>`;
                }
            }
        ]
    });
});


function Delete (url){
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    console.log("RELAOD SUCCESS");
                    DataTable.ajax.reload();
                    window.location.reload();
                    toastr.success(data.message);
                },
                failure: function () {
                    console.log("ERROR");
                    toastr.error("Error while deleting product");
                }
            })
        }
    })
}

