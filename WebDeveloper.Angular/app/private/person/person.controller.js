(function () {
    'user strict';

    angular.module('app')
        .controller('personController', personController);


    personController.$inject = ['dataService'];

    function personController(dataService) {
        var vm = this;
        vm.title = 'Person Controller';
        var apiUrl = 'http://localhost/WebDeveloper.API/Person/';
        //variables
        vm.personList = [];
        vm.person;
        vm.readOnly = false;
        vm.isDelete = false;
        vm.buttonTitle = "";
        vm.totalRows = 0;
        vm.maxSize = 10;
        vm.rowSize = 15;
        vm.currentPage = 1;
        //function
        vm.create = create;
        vm.update = update;
        vm.details = details;
        vm.delete = modalDelete;
        vm.getPersonDetail = getPersonDetail;
        vm.personUpdate = personUpdate;
        vm.pageChanged = pageChanged;
        vm.setRowSize = setRowSize;

        vm.modalFunction;


        init();

        function init() {
            totalRows();
            loadData();
        }

        function totalRows() {
            var url = apiUrl + '/totalrows';
            dataService.getData(url)
                .then(function (result) {
                    vm.totalRows = result.data;
                },
                function (error) {
                    console.log(error);
                });
        }

        function pageChanged() {
            vm.personList = [];
            loadData();

        }

        function setRowSize(rowSize) {
            if (vm.rowSize === rowSize) return;
            vm.rowSize = rowSize;
            vm.currentPage = 1;
            loadData();

        }

        function loadData() {
            vm.personList = [];
            var url = apiUrl + 'list/'+vm.currentPage+'/'+vm.rowSize;
            dataService.getData(url)
                .then(function (result) {
                    vm.personList = result.data;
                },
                function (error) {
                    console.log(error);
                });
        }

        function getPersonDetail(id) {
            var url = apiUrl + id;
            dataService.getData(url).then(
                function (result) {
                    vm.person = result.data;
                }
                );

        }

        function create() {
            vm.person = {};
            vm.modalFunction = personCreate;
            vm.readOnly = false;
            vm.isDelete = false;
            vm.buttonTitle = "Create"
        }

        function update() {
            vm.modalFunction = personUpdate;
            vm.buttonTitle = "Update";
            vm.readOnly = false;
            vm.isDelete = false;
        }

        function modalDelete() {
            vm.readOnly = true;
            vm.isDelete = true;
            vm.modalFunction = personDelete;
        }

        function personCreate() {
            dataService.putData(apiUrl, vm.person)
                .then(function () {
                    loadData();
                    closeModal();
                    totalRows();
                })
        }

        function personUpdate() {
            dataService.postData(apiUrl, vm.person)
                .then(function () {
                    loadData();
                    closeModal();
                    totalRows();
                })
        }

        function personDelete() {
            dataService.deleteData(apiUrl + vm.person.businessEntityID)
                .then(function () {
                    loadData();
                    closeModal();
                    totalRows();
                })
        }
       
        function details() {
            vm.readOnly = true;
            vm.isDelete = false;
        }
        function closeModal() {
            $('button[data-dismiss="modal"]').click();
        }


    }

})();