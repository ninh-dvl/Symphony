    $(document).ready(function () {
      
          /*ALIGN HEIGHT PRODUCT*/
          function alignHeight(selector) {
  
              jQuery(selector).css('height', '');
              var minHeight = 0;
  
              jQuery(selector).each(function () {
                  if (minHeight < jQuery(this).outerHeight()) {
                      minHeight = jQuery(this).outerHeight();
                  }
              });
  
              if (minHeight > 0) {
                  jQuery(selector).css('height', minHeight);
              }
          }
  
          setTimeout(function () {
              alignHeight('.lecturer .description');
              alignHeight('.single-service h2');
              alignHeight('.courses .course-content p');
        }, 1000);

        $('.header-page nav .desktop-nav li').click(function () {
            $(this).addClass("active").siblings().removeClass("active");
        });
      });

  
  
  