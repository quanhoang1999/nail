var homeController = function () {
    this.initialize = function () {
        registerEvents();
        loadData();
        $('#homepageinfo').html(`<div class="col-sm-12 col-md-12 col-lg-12 home-section-title">
                    <h3 class="title-type">Welcome to</h3>
                    <h2 class="title">Lougi Nails Spa</h2>
                    <h3 class="title-type">Where Beauty Meets Care!</h3>
                    <div class="line-bottom-title">
                        <span class="line-left"></span>
                        <img src="images/icon/icon-title.png" />
                        <span class="line-right"></span>
                    </div>
                    <h3 class="descript-body text-center">
                        At Lougi Nails Spa, we take pride in being your go-to nail salon, where your beauty needs are met with exceptional care and attention to detail. We may not be the biggest or the most extravagant, but we believe in providing a warm and inviting atmosphere that sets the stage for a relaxing and enjoyable experience.
                    </h3>
                    <div class="descript-body info">
                        <h4>Our Commitment to Excellence</h4>
                        <ul>
                            <li>
                                <b>Comprehensive Services</b>: Discover a wide range of nail services designed to enhance and beautify your hands and feet. Whether you're looking for a classic manicure, a trendy nail design, or a rejuvenating pedicure, our skilled technicians are here to bring your vision to life.
                            </li>
                            <li>
                                <b>Expert Technicians</b>: At [Your Salon Name], our technicians are more than just nail artists – they are dedicated professionals who are passionate about their craft. Trained in the latest techniques and trends, our team is committed to delivering results that exceed your expectations.
                            </li>
                            <li>
                                <b>Clean and Inviting Atmosphere</b>: Step into a clean and welcoming space that prioritizes your comfort. We understand the importance of a hygienic environment, and that's why we go the extra mile to maintain the highest standards of cleanliness. Our salon is your sanctuary, where you can unwind and indulge in some well-deserved self-care.
                            </li>
                            <li>
                                <b>Sanitization and Safety</b>: Your health and safety are our top priorities. Rest assured that we follow rigorous sanitization practices, including the thorough cleaning and sterilization of tools after each customer. Feel confident knowing that you are in a salon that values your well-being.
                            </li>
                            <li>
                                <b>Bringing the Best to You</b>: Despite our humble size, we are big on delivering the best services to our valued customers. From the quality of our products to the expertise of our staff, we are dedicated to creating an experience that leaves you feeling pampered and refreshed.
                            </li>
                            <li>
                                At [Your Salon Name], we believe that beauty is not just about aesthetics; it's about the care and attention you give to yourself. Join us in our cozy haven, and let us enhance your natural beauty with our expert services and personalized touch.
                            </li>
                            <li>
                                <b>Indulge. Relax. Enjoy. Discover the beauty of care at Lougi Nails Spa.</b>
                            </li>

                        </ul>
                    </div>
                </div>`);
        
      
    }
    function registerEvents() {
        //$('.gallery1').nivoLightbox();
    
    $("#closenoti").click(function () {
        $("#modalNotification").css("display", "none");
    });
}
 
    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
               
            },
            url: '/service/en/index',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $.each(response.Results, function (i, p) {
                    render += Mustache.render(template, {
                        Name: p.Item.Name,
                        Description: p.Item.Description
                    });
                });
                if (render != '') {
                    $('#table-template').html(render);
                }
               
            },
            error: function (status) {
                
            }
        });
    }
}

 
