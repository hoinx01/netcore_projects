<template>
    <div>
        <div id="wheel-container">
            <div class="wheel">
                <!--<div class="wheel_pointer">
                </div>-->
            </div>
            <div>
                <b-btn v-b-modal.upload_wheel class="upload-button">Upload Vòng quay</b-btn>
                <b-btn v-b-modal.upload_wheel_pointer class="upload-button">Upload Kim</b-btn>
                <b-btn>Quay thử</b-btn>
            </div>
        </div>
        

    </div>
</template>
<script>
    export default {
        name: 'lucky-wheel',
        data() {
            return {
                count: 6,
                r: 300,
                rootCircle: {
                    x: 450,
                    y: 450
                }
            }
        }, 
        computed: {
            angle() {
                return Math.PI / 6;
            },
            transforms() {
                let angle = 30;
                let angleInPi = angle / 180 * Math.PI;
                let x = this.rootCircle.x;
                let y = this.rootCircle.y;

                let newX = x * Math.cos(angleInPi) - y * Math.sin(angleInPi);
                let newY = y * Math.cos(angleInPi) + x * Math.sin(angleInPi);

                console.log(newX)
                console.log(newY)

                let translateX = x - newX;
                let translateY = y - newY;

                let style = {
                    transform: 'rotate(' + angle.toString() + ')' + ' translate(' + translateX.toString() + ',' + translateY.toString() + ')'
                }
                console.log(style)
                return style;
            },
            root() {
                let baseRad = Math.PI * 2 / this.count;
                let halfRad = baseRad / 2;
                let x = this.rootCircle.x;
                let y = this.rootCircle.y;
                let r = this.r;

                let p0Raw = x.toString() + "," + y.toString();
                let p1 = {
                    x: x - r * Math.sin(halfRad),
                    y: y - r * Math.cos(halfRad)
                };
                let p1Raw = p1.x.toString() + "," + p1.y.toString();
                let p2 = {
                    x: x,
                    y: y - r
                };
                let p2Raw = p2.x.toString() + "," + p2.y.toString();
                let p3 = {
                    x: x + r * Math.sin(halfRad),
                    y: y - r * Math.cos(halfRad)
                };
                let p3Raw = p3.x.toString() + "," + p3.y.toString();

                let pathRaw = "M" + p0Raw + " L" + p1Raw + " C" + p1Raw + " " + p2Raw + " " + p3Raw + " Z";
                return pathRaw;
            }
        }
    }
</script>
<style scoped>
    #wheel-container{
        width:300px;
        height:300px;
        background-color:red;
    }
    .wheel{
        width:300px;
        height:300px;
        border-radius: 150px;
        background-color: yellow;
        display:flex;
        align-items:center;
        justify-content:center;
        background-image: url(http://static.ekipvn.com/Sites/6637/Data/images/2014/3/v%C3%B2ng%20quay.jpg);
        background-position: center;
        background-size: cover;
    }
    .wheel_pointer{
        width:100px;
        height:100px;
        border-radius: 50%;
        background-color: white
    }
</style>