jQuery(document).ready(function() {
	function isScrolledTo(elem,top) {
		var docViewTop = jQuery(window).scrollTop(); //num of pixels hidden above current screen
		var docViewBottom = docViewTop + jQuery(window).height();

		var elemTop = jQuery(elem).offset().top - top; //num of pixels above the elem

		return ((elemTop <= docViewTop));
	}

	function stickThatMenu(sticky,catcher,top) {
		if(isScrolledTo(sticky,top)) {
			sticky.addClass('sticky-navigation-active');
			catcher.height(sticky.height());
		} 
		var stopHeight = catcher.offset().top;
		if ( stopHeight > sticky.offset().top) {
			sticky.removeClass('sticky-navigation-active');
			catcher.height(0);
		}
	}

	var catcher = jQuery('#catcher'),
		sticky  = jQuery('.sticky-navigation'),
		bodyTop = jQuery('body').offset().top;

	if ( sticky.length ) {
	
		jQuery(window).scroll(function() {
			stickThatMenu(sticky,catcher,bodyTop);
		});
		jQuery(window).resize(function() {
			stickThatMenu(sticky,catcher,bodyTop);
		});
	}

	/*----------------------------------------------------
	/* Social button scripts
	/*---------------------------------------------------*/
	jQuery.fn.exists = function(callback) {
		var args = [].slice.call(arguments, 1);
		if (this.length) {
			callback.call(this, args);
		}
		return this;
	};
	jQuery('#catchersocial').exists(function() {

		var catcherSocial   = jQuery('#catchersocial'),
			catcherSocial2  = jQuery('#catchersocial2'),
			stickySocial    = jQuery('.shareit.floating'),
			stickyNavHeight = 0,
			stickySocialTop = 0;

		if ( sticky.length ) {
			stickyNavHeight = sticky.height();
		}

		stickySocialTop = stickyNavHeight + bodyTop;

		jQuery(window).scroll(function() {

			if ( isScrolledTo( catcherSocial, stickySocialTop ) || ( isScrolledTo( catcherSocial2, stickySocial.outerHeight() ) && stickySocial.css('position') == 'absolute' ) ) {
				
				stickySocial.css( 'position', 'fixed' );
				stickySocial.css( 'top', stickySocialTop );
				stickySocial.css( 'bottom', 'auto' );
				stickySocial.css( 'width', catcherSocial.width() );
				catcherSocial.height(stickySocial.outerHeight(true));
			}

			var stopHeight  = catcherSocial.offset().top;
			if ( stopHeight > stickySocial.offset().top) {
				stickySocial.css('position','relative');
				stickySocial.css('top', '0');
				stickySocial.css('bottom', 'auto');
				stickySocial.css( 'width', catcherSocial.width() );
				catcherSocial.height(0);
			}

			var stopHeight2 = catcherSocial2.offset().top;
			if ( stopHeight2 < stickySocial.offset().top + catcherSocial.height() + stickySocial.outerHeight(true) ) {
				stickySocial.css('position','absolute');
				stickySocial.css('top', 'auto');
				stickySocial.css( 'width', catcherSocial.width() );
				stickySocial.css('bottom', stickySocial.outerHeight(true));
			}
			
		});

		jQuery(window).resize(function() {
			jQuery('.shareit.floating').css( 'width', jQuery('#catchersocial').width() );
		});
	});
});