

'use strict';
invModule.controller('ItemDistributionController',
[
	'$scope', '$location', '$routeParams',
	'authRepository', 'locationRepository', 'itemRepository', 'appRepository', 'departmentRepository', 'employeeRepository', 'ModalService',

	function ($scope, $location, $routeParams,
		authRepository, locationRepository, itemRepository, appRepository, departmentRepository, employeeRepository, ModalService) {

		$scope.loadEmployee = function(id) {
			$scope.employees = employeeRepository.getEmployeesByDepartmentId(id);
		}

		$scope.loadDistributionAdd = function () {
			$scope.auth = authRepository.authentication;
			
			departmentRepository.getAllDepartment()
				.then(function(response) {
					$scope.departments = response;
				}, function () { });

			$scope.storeLocations = locationRepository.getAllLocations();

			$scope.distribution = {
				authorizedByName: $scope.auth.fullName,
				authorizedBy: $scope.auth.employeeId,
				authorizedDesignation: $scope.auth.designation,
				distributionItems:[]
			};
		};

		$scope.itemDetailHierarchy = [];

		$scope.loadItemDistribution = function loadItemDistribution() {
			
			itemRepository.getItemDistribution($routeParams.serialId)
				.get().$promise
				.then(function (data) {
					$scope.itemDetail = data;
					itemRepository.getItemDistributionHierarchy($routeParams.serialId)
						.query().$promise
						.then(function (response) {
							$scope.itemDetailHierarchy = response;
						}, function (error) {
							console.log(error);
						});
				}, function (err) {
					console.log("error", err);
				});
		};

		$scope.showItemLookup = function () {
			ModalService.showModal({
				templateUrl: "/app/inventory/templates/distribution/item-lookup.html",
				controller: "ItemLookupModalController",
				inputs: {
					title: "Select Item",
					parentId: -1,   // to load item values
					resultData: {}
				}
			}).then(function (modal) {
				modal.element.modal();
				modal.close.then(function (result) {
					$('.modal-backdrop').remove();
					var itemData = result.resultData;
					ModalService.showModal({
						templateUrl: "/app/inventory/templates/distribution/item-serial-lookup.html",
						controller: "ItemLookupModalController",
						inputs: {
							title: "Select Item : " + result.resultData.itemCode + ", " + result.resultData.itemName,
							parentId: result.resultData.itemId,    // to load selected item serials
							resultData: {}
						}
					}).then(function (modal) {
						modal.element.modal();
						modal.close.then(function (result) {
							$('.modal-backdrop').remove();

							if (result.resultData === "back") {
								console.log("back");
								$scope.showItemLookup();
								return;
							}
							else {
								angular.forEach(result.resultData, function (value, key) {
									console.log("value", value);
									$scope.distribution.distributionItems.push({
										itemId: value["itemId"],
										distributionId:0,
										itemStockSerialId: value["itemStockSerialId"],
										itemImage: itemData.itemPicture,
										itemCode: itemData.itemCode,
										itemName: itemData.itemName,
										stockInHand: itemData.stockinHand,
										serialNo: value["serialNo"],
										createdBy: $scope.auth.employeeId,
										createdOn: new Date()
									});
								});
							}
						});
					});
				});
			});
		};

		$scope.saveDistribution = function (distribution) {

			itemRepository.addItemDistribution(distribution)
				.$promise
				.then(function (result) {
					
					appRepository.showAddSuccessGritterNotification();
					$location.url('/INVPortal');

				}, function (error) {
					appRepository.showErrorGritterNotification();
			});
		};

		$scope.removeItemDistribution = function(item) {
			$scope.distribution.distributionItems.splice($scope.distribution.distributionItems.indexOf(item), 1);
		}


		///////////////////////////////////////////////////////
		/// ---   Item Distribution View ------------

		$scope.loadDistributionView = function () {

		};

		var timelineBlocks = $('.cd-timeline-block'),
		offset = 0.8;

		//hide timeline blocks which are outside the viewport
		
		function hideBlocks(blocks, offset) {
			blocks.each(function () {
				($(this).offset().top > $(window).scrollTop() + $(window).height() * offset) && $(this).find('.cd-timeline-img, .cd-timeline-content').addClass('is-hidden');
			});
		}

		function showBlocks(blocks, offset) {
			blocks.each(function () {
				($(this).offset().top <= $(window).scrollTop() + $(window).height() * offset && $(this).find('.cd-timeline-img').hasClass('is-hidden')) && $(this).find('.cd-timeline-img, .cd-timeline-content').removeClass('is-hidden').addClass('bounce-in');
			});
		}

		$scope.gotoDetail = function () {
			$location.url('/INVPortal/item/detail/' + $routeParams.id);
		}


		$scope.getItemBySerial = function () {
			
			if ($routeParams.id !== "") {
				itemRepository.getItemBySerialNo($routeParams.id).query()
					.$promise
					.then(function (response) {

						$scope.itemSerials = response;

					}, function (err) {
						console.log(err);
					});
			}
		};


		//on scolling, show/animate timeline blocks when enter the viewport
		//$(window).on('scroll', function () {
			
		//    (!window.requestAnimationFrame)
		//        ? setTimeout(function () { showBlocks(timelineBlocks, offset); }, 100)
		//        : window.requestAnimationFrame(function () { showBlocks(timelineBlocks, offset); });
		//});

		hideBlocks(timelineBlocks, offset);
	}
]);