<template>
    <b-modal :id="elementId" class="modal-cropper">
        <div ref="modal" id="checker"></div>
        <div style="display:flex;flex-direction:row;justify-content:center">
            <!--<div :style="cropperContainerStyle">

        </div>-->
            <vue-cropper ref='cropper'
                         :containerStyle="cropperContainerStyle"
                         :guides="true"
                         dragMode="none"
                         :movable="false"
                         :rotatable="false"
                         :zoomable="false"
                         :scalable="false"
                         :zoomOnWheel="false"
                         :zoomOnTouch="false"
                         :src="image"
                         :autoCrop="true"
                         :cropmove="cropImage"
                         alt="Source Image">
            </vue-cropper>
        </div>




        <div>
            <input type="file" @change="changeImage" />
        </div>
        <div>
            <button @click="cropImage">Crop</button>
            <div>
                <button @click="rotateLeft">RotateLeft</button>
                <button @click="rotateRight">RotateRight</button>
                <button @click="zoomIn">ZoomIn</button>
                <button @click="zoomOut">ZoomOut</button>
            </div>


            <div>
                <button @click="getContainerData">GetContainerData</button>
                <button @click="getImageData">GetImageData</button>
                <button @click="getCanvasData">getCanvasData</button>
                <button @click="getCropboxData">GetCropboxData</button>
                <button @click="getCroppedImage">getCroppedImage</button>
                <button @click="preview">Preview</button>
            </div>
        </div>
        <div><textarea style="width:100%;height:75px;">{{current}}</textarea></div>
        <div :style="croppedImageStyle">
            <img :src="croppedImage" style="height: 100%; width: 100%; object-fit: contain" alt="Cropped Image" />
        </div>
    </b-modal>
</template>
<script>
    import VueCropper from 'vue-cropperjs';
    import uploadClient from '../../Api/CmsBaseApi.js';

    export default {
        name: 'upload-image',
        components: {VueCropper},
        props: {
            elementId: {
                type: String,
                required: true
            },
            imageSizeConfig: {
                type: Object,
                required: true
            },
            cropperContainerConfig: {
                type: Object,
                required: true
            }
        },
        data() {
            return {
                image: '#',
                croppedImage: '',
                current: '',
                standardContainerSize: {
                    width: 700,
                    height: 266
                },
                currentContainerSize: {
                    width: 700,
                    height:266
                },
                cropperContainerStyle: {
                    width: this.cropperContainerConfig.horizontal + 'px',
                    height: this.cropperContainerConfig.horizontal / this.imageSizeConfig.ratio + 'px'
                },
                croppedImageStyle: {
                    width: this.imageSizeConfig.minHorizontal + 'px',
                    height: (this.imageSizeConfig.minHorizontal / this.imageSizeConfig.ratio) + 'px',
                    border: "1px solid gray"
                },
                lastCropBox: {}
            }
        },

        methods: {
            changeImage(e) {
                let files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                this.createImage(files[0]);
            },
            zoomIn() {
                this.$refs.cropper.relativeZoom(0.1);
            },
            zoomOut() {
                this.$refs.cropper.relativeZoom(-0.1);
            },
            rotateLeft() {
                this.$refs.cropper.rotate(-90);
            },
            rotateRight() {
                this.$refs.cropper.rotate(90);
            },
            createImage(file) {
                if (!file.type.includes('image/')) {
                    alert('Please select an image file');
                    return;
                }

                let currentWidth = this.$refs.modal.clientWidth;
                if (currentWidth < this.standardContainerSize.width) {
                    this.currentContainerSize.width = currentWidth;
                    this.currentContainerSize.height = currentWidth * this.standardContainerSize.height / this.standardContainerSize.widht;
                }

                var _URL = window.URL || window.webkitURL;

                let image = new Image();
                image.onload = (event) => {
                    let imageData = this.getImageData();
                    let canvasData = this.getCanvasData();
                    let container = this.getContainerData();
                    this.$refs.cropper.setAspectRatio(this.imageSizeConfig.ratio);

                    let width = imageData.width;
                    let widthByHeight = imageData.height * this.imageSizeConfig.ratio;

                    width = width <= widthByHeight ? width : widthByHeight;
                    let height = width / this.imageSizeConfig.ratio;

                    this.$refs.cropper.setCropBoxData({ width: width, left: (container.width/2 - width/2), top: (container.height/2 - height/2)});
                    this.lastCropBox = { width: imageData.width, left: canvasData.left };
                };

                let reader = new FileReader();

                reader.onload = (e) => {
                    let loadedFile = e.target.result;
                    image.src = _URL.createObjectURL(file);
                    this.$refs.cropper.replace(loadedFile);


                };
                reader.readAsDataURL(file);
            },
            cropImage() {
                let canvas = this.getCanvasData();
                let cropBox = this.getCropboxData();
                let imageData = this.getImageData();

                if (cropBox.left < canvas.left
                    || (cropBox.left + cropBox.width) > (canvas.left + canvas.width)
                    || cropBox.top < 0
                    || (cropBox.top + cropBox.height) > (canvas.top + canvas.height)
                ) {
                    this.$refs.cropper.setCropBoxData(this.lastCropBox);
                    return;
                }

                let croppedImageWidth = cropBox.width * imageData.naturalWidth / imageData.width;
                if (croppedImageWidth < this.imageSizeConfig.minHorizontal) {
                    this.$refs.cropper.setCropBoxData(this.lastCropBox);
                    return;
                }

                this.lastCropBox = { width: cropBox.width, left: cropBox.left };
                //this.croppedImage = croppedCanvas.toDataURL();
                

                //let image = new Image();
                //image.onload = (event) => {
                //    let croppedWidth = image.width;
                //    if (croppedWidth < this.imageSizeConfig.minHorizontal)
                //        this.$refs.cropper.setCropBoxData(this.lastCropBox);
                //    else {
                //        this.lastCropBox = { width: cropBox.width, left: cropBox.left };
                //        this.croppedImage = croppedCanvas.toDataURL()
                //    }
                //};

                //var reader = new FileReader();
                //reader.addEventListener("loadend", function () {
                //    image.src = reader.result;
                //});
                //let img = croppedCanvas.toBlob((blob) => {
                //    reader.readAsDataURL(blob);
                //});
            },
            async preview() {
                let croppedCanvas = this.$refs.cropper.getCroppedCanvas();
                this.croppedImage = croppedCanvas.toDataURL();

                croppedCanvas.toBlob(async (blob) => {
                    let uploadResult = await uploadClient.upload(blob, 'social_topic', this.currentFileName);
                    this.$emit('uploaded', uploadResult.cdnPath)
                })
                
            },
            getContainerData() {
                this.current = JSON.stringify(this.$refs.cropper.getContainerData())
                return this.$refs.cropper.getContainerData();
            },
            getImageData() {
                this.current = JSON.stringify(this.$refs.cropper.getImageData())
                return this.$refs.cropper.getImageData();
            },
            getCanvasData() {
                this.current = JSON.stringify(this.$refs.cropper.getCanvasData())
                return this.$refs.cropper.getCanvasData();
            },
            getCropboxData() {
                this.current = JSON.stringify(this.$refs.cropper.getCropBoxData())
                return this.$refs.cropper.getCropBoxData();
            },
            getCroppedImage() {
                this.current = JSON.stringify(this.$refs.cropper.getCroppedCanvas())
                return this.$refs.cropper.getCroppedCanvas();
            }
        }
    }
</script>