
        function ChangeUrl(title, url) {  
            if (typeof(history.pushState) != "undefined") {  
                var obj = { Title: title, Url: url };  
                history.pushState(obj, obj.Title, obj.Url);  
            } else {  
                alert("Browser does not support HTML5.");  
            }  
        }  

        function settingUpUrl(filters){

            var filterQuery = jQuery.param(filters);
            var enf = encodeURIComponent(filterQuery);

            url = '/?filters=' + enf;
            ChangeUrl('Page1', url);
            // window.location = url
        }

       


       

        function pagedData(pageNo){
             var filter = getFilters(true, pageNo);
             filterData(filter);
             settingUpUrl(filter);
        }

        function filterData(filterPar){

             axios.post('/Home/Filter', filterPar)
                .then(function (response) {
                    console.log(response);
                    bind(response);
                    
                })
                .catch(function (error) {
                    console.log(error);
                });            
        }

        function bind(response){           
            var data = '';
            $.each(response.data.data, function (key, value) {
                var dataRow = 
                    '<section class="my-2" style="p-2 border-radius: .5rem .5rem 0 0;">'+
                    '<div class="row d-flex justify-content-center">'+
                    '<div class="col-12 col-md-12">'+
                    '<div class="card" style="border-radius: 15px;">'+
                    '<div class="card-body p-3">'+
                    '<div class="d-flex">'+
                    '<div class="flex-shrink-1">'+
                    (value.enumNames.gender == 'Male' ? 
                        '<img src="/images/man.png" alt = "Generic placeholder image" class="img-fluid" style = "width: 180px; border-radius: 10px;" >':
                        '<img src="/images/woman.png" alt = "Generic placeholder image" class="img-fluid" style = "width: 180px; border-radius: 10px;" >'
                    )+
                    '</div>'+
                
                    '<div class="flex-grow-1 ms-3">'+
                    '<h5 class="mb-1">'+value.firstName+' '+value.lastName+'</h5>'+
                    '<p class="mb-2 pb-1">' + value.enumNames.job + '</p>'+
                    '<hr />'+

                    '<div class="d-flex flex-wrap justify-content-start rounded-3 p-2 mb-2 bg-body-tertiary">'+
                    '<div class="px-1 pb-2">'+
                    '<p class="small text-muted mb-1">Gender</p>'+
                    '<p class="small mb-0 w-100">' + value.enumNames.gender + '</p>' +
                    '</div>'+
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Civil Status</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.civilStatus + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Age</p>' +
                    '<p class="small mb-0 w-100">' + value.age + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Race</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.race + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Religion</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.religion + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Cast</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.cast + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">City</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.city + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">District</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.district + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Country</p>' +
                    '<p class="small mb-0 w-100">' + value.enumNames.country + '</p>' +
                    '</div>' +
                    '<div class="px-1 pb-2">' +
                    '<p class="small text-muted mb-1">Published</p>' +
                    '<p class="small mb-0 w-100">' + value.createdTimeAgo + '</p>' +
                    '</div>' +

                    '<div class="col-4 col-xs-12 d-flex pt-1">'+
                    '<a href="home/details/'+value.id+'" data-mdb-button-init data-mdb-ripple-init class="btn btn-sm btn-outline-primary me-1 flex-grow-1">Details</a>'+
                    '<a href="#" data-mdb-button-init data-mdb-ripple-init class="btn btn-sm btn-primary flex-grow-1">Connect</a>'+                 
                    '</div>' +

                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</section>';
                    data = data+dataRow;

            });

            $('#filter-list').html('');
            $('#filter-list').append(data);
            page(response);
        }

        function page(response) {
            var pageNos = '';
            for (var i = 1; i <= response.data.totalPages; i++) {
                if (i == response.data.currentPage) {
                    pageNos = pageNos +
                        '<li class="page-item active">' +
                        '<button class="page-link" onclick="pagedData('+i+')">' + i + '</button>' +
                        // '<a class="page-link" href="#">' + i + '</a>' +
                        '</li>';
                } else {
                    pageNos = pageNos +
                        '<li class="page-item">' +
                        '<button class="page-link" onclick="pagedData('+i+')">' + i + '</button>' +
                        // '<a class="page-link" href="#">' + i + '</a>' +
                        '</li>';
                }
            }

            var data = '';
            data =
                '<nav aria-label="Page navigation example">' +
                '<ul class="pagination justify-content-center">' +
                '<li class="page-item">' +
                '<button class="page-link" onclick="pagedData(1)">' +
                // '<a class="page-link" href="#">' +
                '<span aria-hidden="true">&laquo;</span>' +
                '</button>' +
                '</li>' +

                (response.data.currentPage > 1 ?
                    '<li class="page-item">' +
                    '<button class="page-link" onclick="pagedData(' + (response.data.currentPage - 1) + ')">' +
                    // '<a class="page-link" href="">' +
                    '<span aria-hidden="true">&lsaquo;</span>' +
                    '</button>' +
                    '</li>' : ''
                ) +

                (pageNos) +                                       
          
                (response.data.currentPage < response.data.totalPages ?
                    '<li class="page-item">' +
                    '<button class="page-link" onclick="pagedData(' + (response.data.currentPage + 1) + ')">' +
                    // '<a class="page-link" href="">' +
                    '<span aria-hidden="true">&rsaquo;</span>' +
                    '</button>' +
                    '</li>' : ''
                ) +


                '<li class="page-item">' +
                '<button class="page-link" onclick="pagedData(' + response.data.totalPages + ')">' +
                // '<a class="page-link" href="#">' +
                '<span aria-hidden="true">&raquo;</span>' +
                '</button>' +
                '</li>' +
                '</ul>' +
                '</nav>';

            $('#paginage').html('');
            $('#paginage').append(data);
        }

       