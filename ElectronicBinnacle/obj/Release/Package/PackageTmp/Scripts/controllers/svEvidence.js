angular.module('bitacora.controllers.SvEvidence', [])
    .controller('SvEvidenceCtrl', [
        '$scope', '$compile', 'samplesFact' , 
        function ($scope, $compile, samplesFact) {
            var defaultImg = "iVBORw0KGgoAAAANSUhEUgAAAPAAAACGCAIAAABsTJAZAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAWhJREFUeF7t0gENAAAMw6D7FzsN99GABm4QIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQh2wOgVnTDHGFlHwAAAABJRU5ErkJggg==";
            $scope.selectedIndex = 0;
            if ($scope.photos.length)
                $scope.photo = $scope.photos[0];
            else
                $scope.photo = { titulo: '', foto: defaultImg }


            $scope.goNextPhoto = function () {
                var next = ($scope.selectedIndex + 1 >= $scope.photos.length ? 0 : $scope.selectedIndex + 1);
                $scope.goPhoto(next);
            }
            $scope.goPreviousPhoto = function () {
                var next = ($scope.selectedIndex - 1 < 0 ? $scope.photos.length - 1 : $scope.selectedIndex - 1);
                $scope.goPhoto(next);
            }
            $scope.goPhoto = function (index) {
                if (index >= $scope.photos.length || index < 0) return;
                $scope.selectedIndex = index;
                $('.carousel img').fadeOut('fast');
                $scope.photo = $scope.photos[index];
                $('.carousel img').fadeIn('fast');
            }
            $scope.onRemovePhoto = function (photoId) {
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Eliminar Evidencia',
                    width: 450,
                    height: 200,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function () {
                        var content = '<form data-ng-submit="removePhoto(' + photoId + ')">' +
                            '<h2>Cuidado</h2>' +
                            '<div><p>Est&aacute; seguro que desea eliminar completamente la imagen de evidencia?</p></div>' +
                            '<div class="right" style="margin-top: 12px"><button type="submit" class="button default" style="padding: 4px 40px;">Si</button>&nbsp;' +
                            '<button class="button" type="button" onclick="$.Dialog.close()" data-ng-disabled="isLoading()" style="padding: 4px 40px;">No</button></div>' +
                            '</div></form>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            }
            $scope.removePhoto = function (photoId) {
                $.Dialog.close();
                var index = getIndexFromId(photoId, $scope.photos, 'PhotoId');
                $scope.photos.splice(index, 1);
                if (!$scope.photos.length) $scope.photo = { titulo: '', foto: defaultImg }
                else $scope.goPhoto(($scope.photos.length == $scope.selectedIndex ? $scope.selectedIndex - 1 : $scope.selectedIndex));

                samplesFact.removePhoto(photoId)
                    .success(function(data) {
                        if (!data.success)
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    });
            }
            $scope.SamplingState = function (state) {
                if (state == $scope.order.SamplingState)
                    return;
                samplesFact.setSamplingState($scope.order.Id, state).then(function (result) {
                    if (result.data.success) {
                        $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Muestreo Evaluado...', content: "El Muestreo se ha evaluado como " + (state == 1 ? "correcto" : "incorrecto") + "." });
                        $scope.order.SamplingState = state;
                    }
                    else
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                });
            }

        }
    ]);