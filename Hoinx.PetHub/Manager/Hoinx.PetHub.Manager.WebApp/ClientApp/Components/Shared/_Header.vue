<template>
    <div>
        {{textValue}}
    </div>
</template>
<script>
    export default {
        name: "header",
        props: {
            textValue: {
                type: String,
                required: true
            }
        }
        //,
        //data() {
        //    console.log(this.textValue)
        //    return {
        //        text: this.textValue
        //    }
        //}
    }
</script>