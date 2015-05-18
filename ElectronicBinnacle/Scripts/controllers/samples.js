angular.module('bitacora.controllers.samples', [])
    .controller('IndexSamplesCtrl', [
        '$scope', 'sampleInfo', 'paramsFact', '$compile', 'samplesFact', '$http', 'loadingFact',
        function ($scope, sampleInfo, paramsFact, $compile, samplesFact, $http, loadingFact) {
            loadingFact.loadingUp();
            var baseUrl = "";
            var currentSampleView = 0;
            var completeLoaded = [true, true, true, true, true];
            $scope.sampleInfo = { SimpleCount: sampleInfo.SimpleCount, ComplexCount: sampleInfo.ComplexCount }
            $scope.hideInteger = function (number) { return (number == -5000 ? "-" : number); }
            $scope.generationBttmMsg = false;
            
            $scope.sample_plan = sampleInfo.SamplingPlan;
            $scope.qality_control = sampleInfo.QualityControl;
            $scope.binnacle = sampleInfo.Binnacle;

            samplesFact.SamplingOrder(sampleInfo.SampleId)
                .success(function(order) {
                    $scope.order = order;
                    $scope.parametersList = "";
                    var params = [];
                    for (var i = 0; i < order.WorkPackages.length; i++) 
                        for (var j = 0; j < order.WorkPackages[i].Packages.length; j++) 
                            for (var k = 0; k < order.WorkPackages[i].Packages[j].Parameters.length; k++) {
                                var param = order.WorkPackages[i].Packages[j].Parameters[k];
                                if (params.every(function(e) { return e != param.ParameterId; })) {
                                    $scope.parametersList += (params.length ? ', ' : '') + param.Identifier;
                                    params.push(param.ParameterId);
                                }
                            }
                    $scope.parametersList += '.';
                    completeLoaded[0] = true;
                    completeLoaded[3] = true;
                })
                .error(function() {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                })
                .then(function() {
                    if (sampleInfo.SimpleCount)
                        initSimple();
                    else
                        initComplex();
                    initString();
                    init();
                    loadingFact.loadingReset();
            });

            $scope.seeSimples = function() {
                if (currentSampleView != 0) {
                    $scope.selectedTemplate.path = 'PartialRoute/svsimplesamples';
                    initSimple();
                }
            }
            $scope.seeComplex = function () {
                if (currentSampleView != 1) {
                    $scope.selectedTemplate.path = 'PartialRoute/svcomplexsample';
                    initComplex();
                }
            }

            
            $scope.protectionToolkit = [
                { name: "Casco", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(0)) },
                { name: "Mascarilla", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(1)) },
                { name: "Lentes", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(2)) },
                { name: "Chaleco  Salvavidas", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(3)) },
                { name: "Overall", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(4)) },
                { name: "Botas", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(5)) },
                { name: "Guantes Cuero", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(6)) },
                { name: "Tyvex", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(7)) },
                { name: "Guantes Latex", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(8)) },
                { name: "Guantes  Nitrilo", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(9)) },
                { name: "Arn\u00E9s", value: $scope.sample_plan.equipoProteccionList.some(toolEqual(10)) }
            ];
            $scope.calibrationData = [];
            var calibrationObjs = [
                { obj: $scope.qality_control.condElectricaDulce, name: "Cond. El\u00E9ctrica \u00B5homs/cm Dulce" },
                { obj: $scope.qality_control.condElectricaSalina, name: "Cond. El\u00E9ctrica \u00B5homs/cm Salina" },
                { obj: $scope.qality_control.bufferPH4, name: "Buffer pH 4 UpH" },
                { obj: $scope.qality_control.bufferPH7, name: "Buffer pH 7 UpH" },
                { obj: $scope.qality_control.bufferPH10, name: "Buffer pH 10 UpH" },
                { obj: $scope.qality_control.potencialREDOX, name: "Potencial REDOX mV" }];
            var calibrationLimits = [75, 130, 0.025, 2.5, 0.02, 10];
            for (var j = 0; j < calibrationObjs.length; j++) {
                var calObj = calibrationObjs[j].obj;
                $scope.calibrationData.push({
                    name: calibrationObjs[j].name,
                    Marca: calObj.marca,
                    Lote: calObj.lote,
                    Caducidad: calObj.caducidad,
                    VM1: $scope.hideInteger(calObj.calibracionInicial.VM.toFixed(2)),
                    TEMP1: $scope.hideInteger(calObj.calibracionInicial.temperatura.toFixed(2)),
                    VB1: $scope.hideInteger(calObj.calibracionInicial.VB.toFixed(2)),
                    CumpleVal1: $scope.hideInteger(calObj.calibracionInicial.VB == -5000 || calObj.calibracionInicial.VM == -5000 ? -5000 : (calObj.calibracionInicial.VB - calObj.calibracionInicial.VM).toFixed(2)),
                    Cumple1: cumple(calObj.calibracionInicial.VB, calObj.calibracionInicial.VM, j),
                    VM2: $scope.hideInteger(calObj.calibracionFinal.VM.toFixed(2)),
                    TEMP2: $scope.hideInteger(calObj.calibracionFinal.temperatura.toFixed(2)),
                    VB2: $scope.hideInteger(calObj.calibracionFinal.VB.toFixed(2)),
                    CumpleVal2: $scope.hideInteger(calObj.calibracionFinal.VB == -5000 || calObj.calibracionFinal.VM == -5000 ? -5000 : (calObj.calibracionFinal.VB - calObj.calibracionFinal.VM).toFixed(2)),
                    Cumple2: cumple(calObj.calibracionFinal.VB, calObj.calibracionFinal.VM, j),
                    visible: calObj.isPresente
                });
            }
            function cumple(vb, vm, index) {
                if (vb == -5000 || vm == -5000)
                    return false;
                return Math.abs(vb - vm) <= calibrationLimits[index];
            }
            $scope.selectedTemplate = {
                path: baseUrl + "PartialRoute/svsampleplan",
                pageIndex : 1,
                option_table: 1,
                quality_option: 1,
            };


            $scope.goPage = function(paginationInfo, index) {
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);
                var completePagination = function() {
                    paginationInfo.currentIndex = nextIndex;
                    var top = Math.min(paginationInfo.currentIndex + 2, paginationInfo.last - 1);
                    var low = Math.max(paginationInfo.currentIndex - 2, 0);
                    paginationInfo.top = top + (2 - (paginationInfo.currentIndex - low));
                    paginationInfo.low = low - (2 - (top - paginationInfo.currentIndex));
                }

                if (currentSampleView == 0)
                    samplesFact.SimpleSample($scope.order.Id, nextIndex)
                        .success(function(session) {
                            $scope.selectedSample = session;
                            $scope.params = [];

                            for (var i = 0; i < session.parametrosMuestraList.length; i++) {
                                var packVerification = session.parametrosMuestraList[i];
                                for (var j = 0; j < packVerification.parameters.length; j++) {
                                    var parameterVerification = packVerification.parameters[j];
                                    var param = getParameterFromOrder($scope.order, parameterVerification.ParameterId);
                                    $scope.params.push({ Parameter: param, PackId: packVerification.packageId, Verified: parameterVerification.verificacion });
                                }
                            }
                            completePagination();
                        })
                        .error(function() {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });

                        });
                else
                    samplesFact.ComplexSample($scope.order.Id, nextIndex)
                        .success(function(session) {
                            $scope.selectedSample = session;
                            $scope.params = [];
                            for (var i = 0; i < session.parametrosMuestraList.length; i++) {
                                var packVerification = session.parametrosMuestraList[i];
                                for (var j = 0; j < packVerification.parameters.length; j++) {
                                    var parameterVerification = packVerification.parameters[j];
                                    var param = getParameterFromOrder($scope.order, parameterVerification.ParameterId);
                                    $scope.params.push({ Parameter: param, PackId: packVerification.packageId, Verified: parameterVerification.verificacion });
                                }
                            }
                            completePagination();
                        })
                        .error(function() {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        });
            };
            $scope.go_previous = function () {
                if ($scope.selectedTemplate.pageIndex != 1) {
//                    if ($scope.isLoading($scope.selectedTemplate.pageIndex - 1)) {
//                        setTimeout($scope.go_previous, 300);
//                        return;
//                    }

                    $scope.selectedTemplate.pageIndex--;
                    $scope.selectedTemplate.path = getSampleTemplatePath(baseUrl, $scope.selectedTemplate.pageIndex, currentSampleView == 0);
                    $scope.selectedTemplate.option_table = 1;
                    $('#section-stepper').stepper('prior');
                }
            };
            $scope.go_next = function() {
                if ($scope.selectedTemplate.pageIndex != 5) {
//                    if ($scope.isLoading($scope.selectedTemplate.pageIndex + 1)) {
//                        setTimeout($scope.go_next, 300);
//                        return;
//                    }

                    $scope.selectedTemplate.pageIndex++;
                    $scope.selectedTemplate.path = getSampleTemplatePath(baseUrl, $scope.selectedTemplate.pageIndex, currentSampleView == 0);
                    $scope.selectedTemplate.option_table = 1;
                    $('#section-stepper').stepper('next');
                }
            };
            $scope.jumpMany = function (pageIndex) {
                if ($scope.selectedTemplate.pageIndex == pageIndex) return;
//                if ($scope.isLoading(pageIndex)) {
//                    setTimeout(function () { $scope.jumpMany(pageIndex); }, 300);
//                    return;
//                }

                var currentIndex = $scope.selectedTemplate.pageIndex;
                var nextIndex = pageIndex;

                $scope.selectedTemplate.pageIndex = Number(pageIndex);
                $scope.selectedTemplate.path = getSampleTemplatePath(baseUrl, $scope.selectedTemplate.pageIndex, currentSampleView == 0);
                $scope.selectedTemplate.option_table = 1;

                if (currentIndex < nextIndex)
                    for (var i = 0; i < nextIndex - currentIndex; i++)
                        $('#section-stepper').stepper('next');
                else
                    for (var i = 0; i < currentIndex - nextIndex; i++)
                        $('#section-stepper').stepper('prior');
            }


            $scope.see_croquis = function (croquisId) {
                samplesFact.Croquis(sampleInfo.SampleId, croquisId)
                    .success(function(croquis) {
                        if (!croquis || !croquis.croquis) {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "No hay ning&uacute;n croquis asociado." });
                            return;
                        }
                        $scope.croquis = croquis;
                        $scope.croquisImg = { img: croquis.croquis , type: 0};
                        $.Dialog({
                            shadow: true,
                            overlay: true,
                            draggable: true,
                            icon: '<span class="icon-droplet"></span>',
                            title: 'Croquis',
                            width: 750,
                            padding: 10,
                            content: 'This Window is draggable by caption.',
                            onShow: function() {
                                var content = '<div>' +
                                    '<div class="image-container shadow text-center" style="height:400px;width:100%;margin:auto;background-color: #eee;"><img class="text-center" src="data:image/png;base64,{{croquisImg.img}}" style="height:400px;width:500px;background: #efefef"></div>' +
                                    '<div data-ng-if="croquis.usoDispositivoAuxiliar" style="padding: 10px 0 0 10px;"><a href="" class="right blue" data-ng-click="toogleImg()">Ver <span data-ng-if="croquisImg.type == 0">Imagen del Dispositivo</span><span data-ng-if="croquisImg.type != 0">Imagen del Croquis</span></a><span style="color: #60A917;">Ha sido usado un dispositivo auxiliar</span></div>' +
                                    '<div style="margin:10px auto 10px;width:700px;"><table class="question-answer askleft"><tr><td>Latitud : </td><td>{{hideInteger(croquis.latitud)}}</td><td>Longitud : </td><td>{{hideInteger(croquis.longitud)}}</td><td>Velocidad : </td><td>{{hideInteger(croquis.velocidad)}} <span class="measurement-unit" data-ng-hide="croquis.velocidad == -5000">m/s</span></td><td>Precisi&oacute;n : </td><td>{{hideInteger(croquis.precision)}} <span class="measurement-unit" data-ng-hide="croquis.precision == -5000">m</span></td></tr>' +
                                    '<tr><td>Fecha : </td><td>{{getDate(croquis.fecha)}}</td><td style="text-align: left;">Hora : </td><td>{{getTime(croquis.fecha)}}</td></tr></table><div>' +
                                    '</div>';
                                $.Dialog.content(content);
                                $.Metro.initInputs();
                            }
                        });
                        $compile($(".metro.window-overlay"))($scope);
                    });
            };
            $scope.toogleImg = function () {
                if ($scope.croquisImg.type == 1) {
                    $scope.croquisImg.img = $scope.croquis.croquis;
                    $scope.croquisImg.type = 0;
                } else {
                    $scope.croquisImg.img = $scope.croquis.fotoDispositivoAuxiliar;
                    $scope.croquisImg.type = 1;
                }
            }
            $scope.seeGps = function(mapId) {
                $http.get('Orders/GPS?id=' + mapId ).success(function (data) {
                    if (!data.gps) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "No hay ning&uacute;n mapa asociado." });
                        return;
                    }
                    $scope.gps = data.gps;
                    $.Dialog({
                        shadow: true,
                        overlay: true,
                        draggable: true,
                        icon: '<span class="icon-droplet"></span>',
                        title: 'Mapas',
                        width: 750,
                        height: 440,
                        padding: 10,
                        content: 'This Window is draggable by caption.',
                        onShow: function() {
                            var content = '<div>' +
                                '<div class="image-container shadow text-center" style="height:400px;width:100%;margin:auto;background-color: #eee;"><img class="text-center" src="data:image/png;base64,{{gps}}" style="height:400px;width:500px;background: #efefef"></div>' +
                                '</div>';
                            $.Dialog.content(content);
                            $.Metro.initInputs();
                        }
                    });
                    $compile($(".metro.window-overlay"))($scope);
                });
            }

            $scope.average = function(array) {
                var sum = 0;
                for (var i = 0; i < array.length; i++) {
                    if (array[i] == -5000)return -5000;
                    sum += array[i];
                }
                return (sum / i).toFixed(2);
            };
            $scope.complexSequenceExpensiveAverage = function() {
                return $scope.average($scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList.map(function (e) { return e.gasto; }));
            };
            $scope.getWatterKing = function(number) {
                switch (number) {
                    case 0:
                        return "AgP";
                    case 1:
                        return "AgR";
                    case 2:
                        return "AgN";
                    case 3:
                        return "AgS";
                    case 4:
                        return "AgEst";
                    case 5:
                        return "AgMar";
                default:
                }
            }
            $scope.individualVolume = function(individualVar) {
                if (individualVar.gasto == -5000 || $scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido == -5000 || $scope.complexSequenceExpensiveAverage() == -5000)
                    return -5000;
                var d = $scope.complexSequenceExpensiveAverage() * $scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList.length;
                if (d == 0) return -5000;
                return individualVar.gasto * $scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido / d;
            }
            $scope.individualVolumePercent = function (individualVar) {
                var result = $scope.individualVolume(individualVar);
                if (result == -5000 || $scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido == -5000 || $scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido == -5000)
                    return -5000;
                return (result / $scope.selectedSample.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido) * 100;
            }
            $scope.watterVolume = function() {
                if (!$scope.selectedSample.pozoAS || $scope.selectedSample.pozoAS.volumenTubo == -5000 || $scope.selectedSample.pozoAS.volumenFiltro == -5000)
                    return '-';
                return $scope.selectedSample.pozoAS.volumenTubo + $scope.selectedSample.pozoAS.volumenFiltro;
            }
            $scope.extractVolume = function() {
                if (!$scope.selectedSample.pozoAS || $scope.selectedSample.pozoAS.volumenTubo == -5000 || $scope.selectedSample.pozoAS.volumenFiltro == -5000)
                    return '-';
                return ($scope.selectedSample.pozoAS.volumenTubo + $scope.selectedSample.pozoAS.volumenFiltro) / 3;
            }
            
            $scope.reportGeneration = function (exportTo) {
                if ($scope.selectedTemplate.pageIndex < 5) {
                    window.open('/Orders/GenerateExel/' + $scope.order.Id + '?exportTo=' + exportTo + '&exportFrom=' + $scope.selectedTemplate.pageIndex);
                }
                $scope.generationBttmMsg = false;
            }
            $scope.check = function (row, head) {
                var currentPackId = head.packId;
                if (!row.verificacionParametros || row.verificacionParametros.every(function (e) { return e.packageId != currentPackId; }))
                    return null;
                var headerPack = head.id;
                var rowPack = null;
                for (var i = 0; i < row.verificacionParametros.length; i++) {
                    rowPack = row.verificacionParametros[i];
                    if (rowPack.packageId == currentPackId)
                        //rowPack = rowPack.parameters;
                        break;
                }
                if (!rowPack)return false;
                for (var i = 0; i < headerPack.length; i++) {
                    var headerParamId = headerPack[i];
                    if (rowPack.parameters.every(function (e) { return !e.verificacion || e.ParameterId != headerParamId; }))
                        return false;
                }
                
                return true;
            }

            function initSimple() {
                currentSampleView = 0;
                samplesFact.SimpleSample($scope.order.Id, 0)
                    .success(function(session) {
                        $scope.selectedSample = session;
                        $scope.params = [];

                        for (var i = 0; i < session.parametrosMuestraList.length; i++) {
                            var packVerification = session.parametrosMuestraList[i];
                            for (var j = 0; j < packVerification.parameters.length; j++) {
                                var parameterVerification = packVerification.parameters[j];
                                var param = getParameterFromOrder($scope.order, parameterVerification.ParameterId);
                                $scope.params.push({ Parameter: param, PackId: packVerification.packageId, Verified: parameterVerification.verificacion });
                            }
                        }
                        $scope.simplePagination = {
                            currentIndex: 0,
                            last: sampleInfo.SimpleCount,
                            pagination: [],
                            top: 4,
                            low: 0
                        }
                        for (var i = 0; i < sampleInfo.SimpleCount ; i++)
                            $scope.simplePagination.pagination.push(i);
                        completeLoaded[1] = true;
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });

                    });
            }
            function initComplex() {
                currentSampleView = 1;
                samplesFact.ComplexSample($scope.order.Id, 0)
                    .success(function(session) {
                        $scope.selectedSample = session;
                        $scope.params = [];
                        
                        for (var i = 0; i < session.parametrosMuestraList.length; i++) {
                            var packVerification = session.parametrosMuestraList[i];
                            for (var j = 0; j < packVerification.parameters.length; j++) {
                                var parameterVerification = packVerification.parameters[j];
                                var param = getParameterFromOrder($scope.order, parameterVerification.ParameterId);
                                $scope.params.push({ Parameter: param, PackId: packVerification.packageId, Verified: parameterVerification.verificacion });
                            }
                        }
                        $scope.simplePagination = {
                            currentIndex: 0,
                            last: sampleInfo.ComplexCount,
                            pagination: [],
                            top: 4,
                            low: 0
                        }
                        for (var i = 0; i < sampleInfo.ComplexCount ; i++)
                            $scope.simplePagination.pagination.push(i);
                        completeLoaded[1] = true;
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    });
            }
            function initString() {
                samplesFact.SamplingString(sampleInfo.SampleId)
                    .success(function(session) {
                        $scope.sample_string = session.SampleString;
                        $scope.complexSamplesIdentifiers = getSamplesIdentifier(session.ComplexSamples);
                        $scope.simpleSamplesIdentifiers = getSamplesIdentifier(session.SimpleSamples);
                        completeLoaded[2] = true;
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    })
                    .then(function() {
                        samplesFact.Photos(sampleInfo.SampleId, 0)
                            .success(function(evidences) {
                                $scope.photos = [];
                                var photosIds = evidences;
                                for (var i = 0; i < photosIds.length; i++)
                                    samplesFact.Photos(sampleInfo.SampleId, photosIds[i])
                                        .success(function(data) {
                                            if (data != "")
                                                $scope.photos.push(data);
                                        })
                                        .error(function() {
                                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                                        });
                                completeLoaded[4] = true;
                            })
                            .error(function() {
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                            });
                    });
            }

            function getParameterFromOrder(order, paramId) {
                for (var i = 0; i < order.WorkPackages.length; i++) {
                    var wp = order.WorkPackages[i];
                    for (var j = 0; j < wp.Packages.length; j++) {
                        var pack = wp.Packages[j];
                        for (var k = 0; k < pack.Parameters.length; k++) {
                            var param = pack.Parameters[k];
                            if (param.ParameterId == paramId)
                                return param;
                        }
                    }
                }
            }
            function init() {
                $scope.siteTypes = [];
                if ($scope.order.ClientData.BillReport)
                    $scope.order.BillerClient = $scope.order.ClientData;

                var sites = ["Llave", "Garraf\u00F3n", "Registro", "C\u00E1rcamo", "Tubo", "Noria", "L\u00F3tico", "L\u00E9ntico", "Pozo Monitoreo", "Estuario", "Laguna Costera", "Orilla", "Costafuera", "Otro"];
                for (var i = 0; i < sites.length; i++)
                    $scope.siteTypes.push({ name: sites[i], value: false, visible: false });
                for (var i = 0; i < $scope.sample_plan.tipoSitioMuestreoList.length; i++) {
                    $scope.siteTypes[$scope.sample_plan.tipoSitioMuestreoList[i].tipoSitio].value = true;
                    if ($scope.sample_plan.tipoSitioMuestreoList[i].tipoSitio == 13)
                        $scope.siteTypes[13].name = $scope.sample_plan.tipoSitioMuestreoList[i].otroSitio;
                }
                $scope.siteTypes[13].visible = true;
                switch ($scope.order.SamplingData.SamplingKind) {
                    case 0:
                        $scope.siteTypes[0].visible = true;
                        $scope.siteTypes[1].visible = true;
                        $scope.siteTypes[5].visible = true;
                        break;
                    case 1:
                        $scope.siteTypes[2].visible = true;
                        $scope.siteTypes[3].visible = true;
                        $scope.siteTypes[4].visible = true;
                        $scope.siteTypes[5].visible = true;
                        break;
                    case 2:
                        $scope.siteTypes[6].visible = true;
                        $scope.siteTypes[7].visible = true;
                        break;
                    case 3:
                        $scope.siteTypes[8].visible = true;
                        break;
                    case 4:
                        $scope.siteTypes[9].visible = true;
                        $scope.siteTypes[10].visible = true;
                        break;
                    case 5:
                        $scope.siteTypes[11].visible = true;
                        $scope.siteTypes[12].visible = true;
                        break;
                    default:
                }
                $http.get('Employees/DownloadSign?employeeId=' + $scope.order.Creator.Employee.EmployeeId)
                    .success(function(data) {
                        $scope.order.Creator.Employee.Signature = data.Signature;
                    });
                $http.get('Employees/DownloadSign?employeeId=' + $scope.order.Sampler.EmployeeId)
                    .success(function (data) {
                        $scope.order.Sampler.Signature = data.Signature;
                    });
            }
            function getSamplesIdentifier(samples) {
                var identifiers = [];
                for (var i = 0; i < samples.length; i++) {
                    var sample = samples[i];
                    for (var j = 0; j < sample.SamplesIdentifier.length; j++) {
                        var identifier = sample.SamplesIdentifier[j];
                        identifier.fechaInicial = sample.GeneralData.fechaInicial;
                        identifier.tipoMuestra = sample.SampleType;
                        identifier.idMuestra = sample.SampleId;
                        identifier.verificacionParametros = sample.ParamVerify;
                        identifiers.push(identifier);
                    }
                    if (sample.SampleType == 1) {
                        identifiers.push({ muestraID: sample.Identifier, fechaInicial: sample.GeneralData.fechaFinal, hora: sample.GeneralData.fechaFinal, opt : true });
                    }
                }
                return identifiers;
            }
            function getSampleTemplatePath(baseUrl, index, simple) {
                var url = baseUrl;
                $scope.generationBttmMsg = false;

                switch (index) {
                    case 1:
                        url += 'PartialRoute/svsampleplan';
                        break;
                    case 2:
                        url += (simple ? 'PartialRoute/svsimplesamples' : 'PartialRoute/svcomplexsample');
                        break;
                    case 3:
                        url += 'PartialRoute/svstring';
                        break;
                    case 4:
                        url += 'PartialRoute/svbinnacle1';
                        break;
                    case 5:
                        url += 'PartialRoute/svevidence';
                        break;
                }
                return url;
            }
        }
    ]);


function toolEqual(index) {
    return function (e) {
        return e.tipo == index;
    }
}
