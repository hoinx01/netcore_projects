<template>
    <div>

        <!--<svg height="800" width="800">
        <g fill="green" stroke="green" stroke-width="5">
            <image xlink:href="https://mdn.mozillademos.org/files/6457/mdn_logo_only_color.png" height="20" width="20" style="z-index:1000" />
            <path d="M0,0 L0,100 C0,100  100,100 100,0 Z"></path>
        </g>
    </svg>-->

        <!--<svg width="500" height="350">
            <defs>
                <clipPath id="myCircle">
                    <circle cx="250" cy="145" r="125" fill="#FFFFFF" />
                </clipPath>
            </defs>
            <image width="500" height="350" xlink:href="https://mdn.mozillademos.org/files/6457/mdn_logo_only_color.png" clip-path="url(#myCircle)" />
        </svg>-->
        <svg height="900" width="900">
            <defs>
                <path id="idPath" :d="root"></path>
            </defs>
            <g fill="green" stroke="green" stroke-width="5" id="g">
                <use x="0" y="0" xlink:href="#idPath" transform="rotate(0)" fill="red"></use>
                <use x="0" y="0" xlink:href="#idPath" transform="rotate(30)" fill="yellow"></use>

            </g>
            <!--<use xlink:href="#g" transform="rotate(20)"></use>-->
        </svg>
        <div>{{transforms}}</div>
        
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