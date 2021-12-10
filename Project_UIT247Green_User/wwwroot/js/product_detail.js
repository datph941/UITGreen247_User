$(document).ready(function () {
    var zoomCollection = '.large-image img';
    $(zoomCollection).elevateZoom({
        zoomType: "inner",
        lensSize: "200",
        easing: true,
        gallery: 'thumb-slider-vertical',
        cursor: 'pointer',
        galleryActiveClass: "active"
    });
    $('.large-image').magnificPopup({
        items: [
            { src: '~/image/demo/shop/product/J9.jpg' },
            { src: '~/image/demo/shop/product/J6.jpg' },
            { src: '~/image/demo/shop/product/J5.jpg' },
            { src: '~/image/demo/shop/product/J4.jpg' },
        ],
        gallery: { enabled: false, preload: [0, 2] },
        type: 'image',
        mainClass: 'mfp-fade',
        callbacks: {
            open: function () {

                var activeIndex = parseInt($('#thumb-slider-vertical .img.active').attr('data-index'));
                var magnificPopup = $.magnificPopup.instance;
                magnificPopup.goTo(activeIndex);
            }
        }
    });
    $("#thumb-slider-vertical .owl2-item").each(function () {
        $(this).find("[data-index='0']").addClass('active');
    });

    $('.thumb-video').magnificPopup({
        type: 'iframe',
        iframe: {
            patterns: {
                youtube: {
                    index: 'youtube.com/', // Loại video
                    id: 'v=', // String that splits URL in a two parts, second part should be %id%
                    src: '//www.youtube.com/embed/%id%?autoplay=1' // URL that will be set as a source for iframe.
                },
            }
        }
    });

    $('.product-options li.radio').click(function () {
        $(this).addClass(function () {
            if ($(this).hasClass("active")) return "";
            return "active";
        });

        $(this).siblings("li").removeClass("active");
        $(this).parent().find('.selected-option').html('<span class="label label-success">' + $(this).find('img').data('original-title') + '</span>');
    });

    var _isMobile = {
        iOS: function () {
            return navigator.userAgent.match(/iPhone/i);
        },
        any: function () {
            return (_isMobile.iOS());
        }
    };

    $(".thumb-vertical-outer .next-thumb").click(function () {
        $(".thumb-vertical-outer .lSNext").trigger("click");
    });

    $(".thumb-vertical-outer .prev-thumb").click(function () {
        $(".thumb-vertical-outer .lSPrev").trigger("click");
    });

    $(".thumb-vertical-outer .thumb-vertical").lightSlider({
        item: 3,
        autoWidth: false,
        vertical: true,
        slideMargin: 15,
        verticalHeight: 340,
        pager: false,
        controls: true,
        prevHtml: '<i class="fa fa-angle-up"></i>',
        nextHtml: '<i class="fa fa-angle-down"></i>',
        responsive: [
            {
                breakpoint: 1199,
                settings: {
                    verticalHeight: 330,
                    item: 3,
                }
            },
            {
                breakpoint: 1024,
                settings: {
                    verticalHeight: 235,
                    item: 2,
                    slideMargin: 5,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    verticalHeight: 340,
                    item: 3,
                }
            }
            ,
            {
                breakpoint: 480,
                settings: {
                    verticalHeight: 100,
                    item: 1,
                }
            }

        ]

    });

    // Xử lý nút đánh giá
    $(".reviews_button,.write_review_button").click(function () {
        var tabTop = $(".producttab").offset().top;
        $("html, body").animate({ scrollTop: tabTop }, 1000);
    });
});