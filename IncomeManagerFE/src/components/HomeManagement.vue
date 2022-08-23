<template>
    <div>
        <q-carousel animated
                    v-model="slide"
                    navigation
                    infinite
                    :autoplay="autoplay"
                    arrows
                    transition-prev="slide-right"
                    transition-next="slide-left"
                    @mouseenter="autoplay = false"
                    @mouseleave="autoplay = true"
                    height="850px">
            <q-carousel-slide :name="1" img-src="https://nebraskalandbank.com/Portals/0/EasyGalleryImages/2823/ImageSlider/Thumbs/iStock585090668.jpg" />
            <q-carousel-slide :name="2" img-src="https://www.mynycb.com/Content/img/Personal/Savings/Overview/myNYCB-savings-overview-1440x738-opt-1.jpg" />
            <q-carousel-slide :name="3" img-src="https://www.newszap.in/wp-content/uploads/2022/01/img3-1.jpg" />
        </q-carousel>
    </div>
</template>

<script>
    import { ref } from 'vue'

    export default {
        name: 'Home-component',
        setup() {
            return {
                slide: ref(1),
                autoplay: ref(true)
            }
        }
    }
</script>