demo = {
  initDocumentationCharts: function() {
    if ($('#dailySalesChart').length != 0 && $('#websiteViewsChart').length != 0) {
      /* ----------==========     Daily Sales Chart initialization For Documentation    ==========---------- */

      dataDailySalesChart = {
        labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
        series: [
          [12, 17, 7, 17, 23, 18, 38]
        ]
      };

      optionsDailySalesChart = {
        lineSmooth: Chartist.Interpolation.cardinal({
          tension: 0
        }),
        low: 0,
        high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
        chartPadding: {
          top: 0,
          right: 0,
          bottom: 0,
          left: 0
        },
      }

      var dailySalesChart = new Chartist.Line('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

      var animationHeaderChart = new Chartist.Line('#websiteViewsChart', dataDailySalesChart, optionsDailySalesChart);
    }
  },

  initDashboardPageCharts: function() {

    if ($('#dailySalesChart').length != 0 || $('#completedTasksChart').length != 0 || $('#websiteViewsChart').length != 0) {
      /* ----------==========     Daily Sales Chart initialization    ==========---------- */

      dataDailySalesChart = {
        labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
        series: [
          [12, 17, 7, 17, 23, 18, 38]
        ]
      };

      optionsDailySalesChart = {
        lineSmooth: Chartist.Interpolation.cardinal({
          tension: 0
        }),
        low: 0,
        high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
        chartPadding: {
          top: 0,
          right: 0,
          bottom: 0,
          left: 0
        },
      }

      var dailySalesChart = new Chartist.Line('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

      md.startAnimationForLineChart(dailySalesChart);



      /* ----------==========     Completed Tasks Chart initialization    ==========---------- */

      dataCompletedTasksChart = {
        labels: ['12p', '3p', '6p', '9p', '12p', '3a', '6a', '9a'],
        series: [
          [230, 750, 450, 300, 280, 240, 200, 190]
        ]
      };

      optionsCompletedTasksChart = {
        lineSmooth: Chartist.Interpolation.cardinal({
          tension: 0
        }),
        low: 0,
        high: 1000, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
        chartPadding: {
          top: 0,
          right: 0,
          bottom: 0,
          left: 0
        }
      }

      var completedTasksChart = new Chartist.Line('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);

      // start animation for the Completed Tasks Chart - Line Chart
      md.startAnimationForLineChart(completedTasksChart);


      /* ----------==========     Emails Subscription Chart initialization    ==========---------- */

      var dataWebsiteViewsChart = {
        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
        series: [
          [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]

        ]
      };
      var optionsWebsiteViewsChart = {
        axisX: {
          showGrid: false
        },
        low: 0,
        high: 1000,
        chartPadding: {
          top: 0,
          right: 5,
          bottom: 0,
          left: 0
        }
      };
      var responsiveOptions = [
        ['screen and (max-width: 640px)', {
          seriesBarDistance: 5,
          axisX: {
            labelInterpolationFnc: function(value) {
              return value[0];
            }
          }
        }]
      ];
      var websiteViewsChart = Chartist.Bar('#websiteViewsChart', dataWebsiteViewsChart, optionsWebsiteViewsChart, responsiveOptions);

      //start animation for the Emails Subscription Chart
      md.startAnimationForBarChart(websiteViewsChart);
    }
  },

  initGoogleMaps: function() {
    var myLatlng = new google.maps.LatLng(40.748817, -73.985428);
    var mapOptions = {
      zoom: 13,
      center: myLatlng,
      scrollwheel: false, //we disable de scroll over the map, it is a really annoing when you scroll through page
      styles: [{
        "featureType": "water",
        "stylers": [{
          "saturation": 43
        }, {
          "lightness": -11
        }, {
          "hue": "#0088ff"
        }]
      }, {
        "featureType": "road",
        "elementType": "geometry.fill",
        "stylers": [{
          "hue": "#ff0000"
        }, {
          "saturation": -100
        }, {
          "lightness": 99
        }]
      }, {
        "featureType": "road",
        "elementType": "geometry.stroke",
        "stylers": [{
          "color": "#808080"
        }, {
          "lightness": 54
        }]
      }, {
        "featureType": "landscape.man_made",
        "elementType": "geometry.fill",
        "stylers": [{
          "color": "#ece2d9"
        }]
      }, {
        "featureType": "poi.park",
        "elementType": "geometry.fill",
        "stylers": [{
          "color": "#ccdca1"
        }]
      }, {
        "featureType": "road",
        "elementType": "labels.text.fill",
        "stylers": [{
          "color": "#767676"
        }]
      }, {
        "featureType": "road",
        "elementType": "labels.text.stroke",
        "stylers": [{
          "color": "#ffffff"
        }]
      }, {
        "featureType": "poi",
        "stylers": [{
          "visibility": "off"
        }]
      }, {
        "featureType": "landscape.natural",
        "elementType": "geometry.fill",
        "stylers": [{
          "visibility": "on"
        }, {
          "color": "#b8cb93"
        }]
      }, {
        "featureType": "poi.park",
        "stylers": [{
          "visibility": "on"
        }]
      }, {
        "featureType": "poi.sports_complex",
        "stylers": [{
          "visibility": "on"
        }]
      }, {
        "featureType": "poi.medical",
        "stylers": [{
          "visibility": "on"
        }]
      }, {
        "featureType": "poi.business",
        "stylers": [{
          "visibility": "simplified"
        }]
      }]

    };
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);

    var marker = new google.maps.Marker({
      position: myLatlng,
      title: "Hello World!"
    });

    // To add the marker to the map, call setMap();
    marker.setMap(map);
  }

}






















//Global Variables Start
var Resume_id_Range = 0;
var Certificate_id_Range = 0;
var Api_Address = window.location.host;
//Global Variables End



//Certificate Event Start
function Onclick_New_Resume_Certificate_Event(e)
{
    var certificates_json = window.document.getElementById("certificates_json");
    var new_certificate_type = window.document.getElementById("new_certificate_type");
    var new_certificate_start = window.document.getElementById("new_certificate_start");
    var new_certificate_end = window.document.getElementById("new_certificate_end");
    var new_certificate_school = window.document.getElementById("new_certificate_school");
    var new_certificate_avrage = window.document.getElementById("new_certificate_avrage");
    var curent_js = JSON.parse(certificates_json.value);
    Certificate_id_Range++;
    curent_js.push({ id: Certificate_id_Range, type: new_certificate_type.value, start: new_certificate_start.value, end: new_certificate_end.value, school: new_certificate_school.value, avg: new_certificate_avrage.value });

    new_certificate_type.value = "";
    new_certificate_start.value = "";
    new_certificate_end.value = "";
    new_certificate_school.value = "";
    new_certificate_avrage.value = "";

    certificates_json.value = JSON.stringify(curent_js);
    Get_Update_Certificate_Table();
}
//Certificate Event End




//New Resume Value Start
function Onclick_New_Resume_Event(e)
{
    var resume_json = window.document.getElementById("resume_json");
    var update_resume_new_companey_name = window.document.getElementById("update_resume_new_companey_name");
    var update_resume_new_companey_website = window.document.getElementById("update_resume_new_companey_website");
    var update_resume_new_companey_job = window.document.getElementById("update_resume_new_companey_job");
    var update_resume_new_companey_start_date = window.document.getElementById("update_resume_new_companey_start_date");
    var update_resume_new_companey_end_date = window.document.getElementById("update_resume_new_companey_until_date");
    var update_resume_new_companey_until_other_ditales = window.document.getElementById("update_resume_new_companey_until_other_ditales");
    var curent_js = JSON.parse(resume_json.value);
    Resume_id_Range++;
    curent_js.push({ id: Resume_id_Range, name: update_resume_new_companey_name.value, website: update_resume_new_companey_website.value, job: update_resume_new_companey_job.value, start: update_resume_new_companey_start_date.value, end: update_resume_new_companey_end_date.value, ditales: update_resume_new_companey_until_other_ditales.value });

    update_resume_new_companey_name.value = "";
    update_resume_new_companey_name.value = "";
    update_resume_new_companey_website.value = "";
    update_resume_new_companey_job.value = "";
    update_resume_new_companey_start_date.value = "";
    update_resume_new_companey_end_date.value = "";
    update_resume_new_companey_until_other_ditales.value = "";

    resume_json.value = JSON.stringify(curent_js);
    Get_Update_Resume_Table();
}
//New Resume Value End





//Get OnClick Delete Certificate Item Start
function OnClick_Delete_Certificate_Item(e, id) {
    var certificates_json = window.document.getElementById("certificates_json");
    var curent_js = JSON.parse(certificates_json.value);
    for (var i = 0; i < curent_js.length; i++) {
        if (curent_js[i].id == id) {
            delete curent_js[i];
            break;
        }
    }
    certificates_json.value = (JSON.stringify(curent_js).replaceAll(",null", "").replaceAll("null,", "").replaceAll("null", ""));
    Get_Update_Certificate_Table();
}
//Get OnClick Delete Certificate Item End



//Get OnClick Delete Resume Item Start
function OnClick_Delete_Resume_Item(e,id)
{
    var resume_json = window.document.getElementById("resume_json");
    var curent_js = JSON.parse(resume_json.value);
    for (var i = 0; i < curent_js.length; i++)
    {
        if(curent_js[i].id==id)
        {
            delete curent_js[i];
            break;
        }
    }
    resume_json.value = (JSON.stringify(curent_js).replaceAll(",null","").replaceAll("null,","").replaceAll("null",""));
    Get_Update_Resume_Table();
}
//Get OnClick Delete Resume Item End



//Get Update Resume Table Start
function Get_Update_Resume_Table()
{
    var resume_json = window.document.getElementById("resume_json");
    var resume_table_body = window.document.getElementById("resume_table_body");

    var js = JSON.parse(resume_json.value);
    resume_table_body.innerHTML = "";
    for(var i=0;i<js.length;i++)
    {
        Resume_id_Range = js[i].id;
        resume_table_body.innerHTML += "<tr><td>" + js[i].name + "</td><td>" + js[i].website + "</td><td>" + js[i].job + "</td><td>" + js[i].start + "</td><td>" + js[i].end + "</td><td>" + js[i].ditales + "</td><td><a style=\"color:#F00;cursor:pointer;\" onclick=\"OnClick_Delete_Resume_Item(this,"+js[i].id+")\">حذف</a></td></tr>";
    }
}
//Get Update Resume Table End



//Get Update Certificate Table Start
function Get_Update_Certificate_Table()
{
    var certificate_json = window.document.getElementById("certificates_json");
    var certificate_table_body = window.document.getElementById("certificate_table_body");

    var js = JSON.parse(certificate_json.value);
    certificate_table_body.innerHTML = "";
    for (var i = 0; i < js.length; i++) {
        Certificate_id_Range = js[i].id;
        certificate_table_body.innerHTML += "<tr><td>" + js[i].type + "</td><td>" + js[i].start + "</td><td>" + js[i].end + "</td><td>" + js[i].school + "</td><td>" + js[i].avg + "</td><td><a style=\"color:#F00;cursor:pointer;\" onclick=\"OnClick_Delete_Certificate_Item(this," + js[i].id + ")\">حذف</a></td></tr>";
    }
}
//Get Update Certificate Table End






//Get Update Resume Table Start
function Get_Update_Resume_Table_View() {
    var resume_json = window.document.getElementById("resume_json");
    var resume_table_body = window.document.getElementById("resume_table_body_view");
    
    var js = JSON.parse(resume_json.value);
    resume_table_body.innerHTML = "";
    for (var i = 0; i < js.length; i++) {
        Resume_id_Range = js[i].id;
        resume_table_body.innerHTML += "<tr><td>" + js[i].name + "</td><td>" + js[i].website + "</td><td>" + js[i].job + "</td><td>" + js[i].start + "</td><td>" + js[i].end + "</td><td>" + js[i].ditales + "</td></tr>";
    }
}
//Get Update Resume Table End



//Get Update Certificate Table Start
function Get_Update_Certificate_Table_View() {
    var certificate_json = window.document.getElementById("certificates_json");
    var certificate_table_body = window.document.getElementById("certificate_table_body_view");

    var js = JSON.parse(certificate_json.value);
    certificate_table_body.innerHTML = "";
    for (var i = 0; i < js.length; i++) {
        Certificate_id_Range = js[i].id;
        certificate_table_body.innerHTML += "<tr><td>" + js[i].type + "</td><td>" + js[i].start + "</td><td>" + js[i].end + "</td><td>" + js[i].school + "</td><td>" + js[i].avg + "</td></tr>";
    }
}
//Get Update Certificate Table End























//City Event Start
function OnChange_Change_State_Event(e)
{
    var state_id = window.document.getElementById("state_id");
    var city_id = window.document.getElementById("city_id");

    var json = new XMLHttpRequest();
    json.onreadystatechange = function (js)
    {
        if (json.statusText == "OK")
        {
            Fill_Cities(json.responseText);
        }
    };
    json.open("GET", "http://" + Api_Address + "/Get_Cities_Of_State/" + state_id.value);
    json.send();
}
//City Event End



//Get Fill City Select Element Start
function Fill_Cities(json)
{
    var state_id = window.document.getElementById("state_id");
    var city_id = window.document.getElementById("city_id");
    var js = JSON.parse(json);
    city_id.innerHTML = "";
    for(var i=0;i<js.length;i++)
    {
        city_id.innerHTML += "<option value=\"" + js[i].id + "\">" + js[i].name + "</option>";
    }
}
//Get Fill City Select Element End



















//Get Search Start
function PerssEnter_Key_OnSearch(event)
{
    if(event.keyCode==13)
    {
        OnCLick_Search_Get_Search()
    }
}
//Get Search End



//Get OnClick Search Button Start
function OnCLick_Search_Get_Search()
{
    var seach_box = window.document.getElementById("seach_box");
    Wait();
    Get_Search(seach_box.value);
}
//Get OnClick Search Button End



//Get Seach Event Start
function Get_Search(Search_Value)
{
    var json = new XMLHttpRequest();
    json.onreadystatechange = function (js)
    {
        if (json.statusText == "OK")
        {
            Show_Search_Result(json.responseText);
        }
    };
    json.open("GET", "http://" + Api_Address + "/Search/" + Search_Value);
    json.send();
}
//Get Seach Event End



//Get Show Search Result Start
function Show_Search_Result(json_result)
{
    var empty = window.document.getElementById("search_result_empty");
    var result = window.document.getElementById("search_result");
    var json = JSON.parse(json_result);

    if(json.length<=0)
    {
        empty.style.display = "block";
        result.style.display = "none";
        Disable_Wait();
    }
    else
    {
        Get_Show_Search_Result_Not_Empty(json_result);
        result.style.display = "block";
        empty.style.display = "none";
        Disable_Wait();
    }

}
//Get Show Search Result End





//Get Show Search Result On Page Content Strat
function Get_Show_Search_Result_Not_Empty(json)
{
    var search_result_not_empty = window.document.getElementById("search_result_not_empty");
    var JS = JSON.parse(json);
    search_result_not_empty.innerHTML = "";

    for (var i = 0; i < JS.length; i++)
    {
        if (JS[i].img == null) {
            search_result_not_empty.innerHTML += "<a target=\"_blank\" class=\"col-md-4\" href='Search_Result/" + JS[i].id + "'><div> <img src=\"/Assets/assets/img/account.png\" style=\"width:120px;height:120px;border-radius:100%;margin: 14px auto;display: block;\"/> <p style=\"margin: 14px auto;display: block;text-align:center;\">" + JS[i].name + " " + JS[i].family + "</p> </div></a>";
        }
        else {
            search_result_not_empty.innerHTML += "<a target=\"_blank\" class=\"col-md-4\" href='Search_Result/" + JS[i].id + "'><div> <img src=\"" + JS[i].img + "\" style=\"width:120px;height:120px;border-radius:100%;margin: 14px auto;display: block;\"/> <p style=\"margin: 14px auto;display: block;text-align:center;\">" + JS[i].name + " " + JS[i].family + "</p> </div></a>";
        }
    }
}
//Get Show Search Result On Page Content End




//Get Dialog Browser Start
function Dialog_Browser(e,url_address)
{
    alert("test");
    var dialog = window.document.getElementById("dialog");
    var dialog_iframe = window.document.getElementById("dialog_iframe");
    dialog.style.display = "flex";
    dialog_iframe.src = url_address;
}

function close_dialog_event(e)
{
    var dialog = window.document.getElementById("dialog");
    dialog.style.display = "none";
}
//Get Dialog Browser End














//Get Wait Start
var Wait_Panel = window.document.getElementById("Wait_Panel");
function Wait()
{
    Wait_Panel.style.display = "block";
}

function Disable_Wait()
{   
    Wait_Panel.style.display = "none";
}
//Get Wait End











//Get Category Data Start
function Get_Category(Category_Id)
{
    var edit_category_panel = window.document.getElementById("edit_category_panel");
    var category_id = window.document.getElementById("category_id");
    var category_title = window.document.getElementById("category_title");
    Wait();

    var XML = new XMLHttpRequest();
    XML.onreadystatechange = function (response) {
        if(XML.statusText=="OK")
        {
            Disable_Wait();
            Get_Edit_Category(XML.responseText);
        }
    };
    XML.open("GET", "http://" + Api_Address + "/Get_Category/" + Category_Id);
    XML.send();

}
//Get Category Data End





//Get Fill Edit Category Panel Start
function Get_Edit_Category(js)
{
    var edit_category_panel = window.document.getElementById("edit_category_panel");
    var category_id = window.document.getElementById("category_id");
    var category_title = window.document.getElementById("category_title");
    var category_status = window.document.getElementById("category_status");
    var json = JSON.parse(js);

    category_id.value = json.id;
    category_title.value = json.title;
    if (json.status == true) {
        category_status.value = "visible";
    } else
    {
        category_status.value = "hide";
    }
    edit_category_panel.style.display = "block";
}
//Get Fill Edit Category Panel End





//Get Edit City Start
function onclick_edit_city(City_Id)
{
    Wait();
    var XML = new XMLHttpRequest();
    XML.onreadystatechange = function (respons) {
        if(XML.statusText=="OK")
        {
            Get_Open_City_Edit_Box(XML.responseText);
        }
    };
    XML.open("GET", "http://" + Api_Address + "/Get_City/" + City_Id);
    XML.send();
}
//Get Edit City End




//Get Fill Edit Box Start
function Get_Open_City_Edit_Box(json)
{
    var updatebox_city = window.document.getElementById("updatebox_city");
    var updatebox_city_name_box = window.document.getElementById("updatebox_city_name_box");
    var updatebox_city_status_box = window.document.getElementById("updatebox_city_status_box");
    var updatebox_city_id_box = window.document.getElementById("updatebox_city_id_box");
    var js = JSON.parse(json);

    updatebox_city.style.display = "block";
    updatebox_city_id_box.value = js.id;
    updatebox_city_name_box.value = js.name;
    if (js.status == true)
    {
        updatebox_city_status_box.value = "visible";
    }
    else
    {
        updatebox_city_status_box.value = "hide";
    }
    Disable_Wait();
}
//Get Fill Edit Box End
















//Get OnClick Edit User Event Start
var Edit_User_Id;
function OnClick_Edit_User(Id)
{
    Edit_User_Id = Id;
    Wait();
    var XML = new XMLHttpRequest();
    XML.onreadystatechange = function (response) {
        if (XML.statusText == "OK") {
            Disable_Wait();
            Get_Edit_User_Box(XML.responseText);
        }
    };
    XML.open("GET", "http://" + Api_Address + "/AllUsers/GetUser/" + Id);
    XML.send();
}

function Get_Edit_User_Box(json)
{
    var user_edit_dialog_main_background = window.document.getElementById("user_edit_dialog_main_background");
    var user_type = window.document.getElementById("user_type");
    var user_status = window.document.getElementById("user_status");
    var js = JSON.parse(json);
    
    if (js.type == "ADMIN")
    {
        user_type.value = "admin";
    }
    else
    {
        user_type.value = "user";
    }

    if (js.status == true)
    {
        user_status.value = "active";
    }
    else
    {
        user_status.value = "diactive";
    }

    user_edit_dialog_main_background.style.display = "flex";
}

function OnClick_Done_Edit_User(e)
{

    var user_edit_dialog_main_background = window.document.getElementById("user_edit_dialog_main_background");
    var user_type = window.document.getElementById("user_type");
    var user_status = window.document.getElementById("user_status");
    var XML = new XMLHttpRequest();
    XML.onreadystatechange = function (response) {
        if(XML.statusText=="OK")
        {
            user_edit_dialog_main_background.style.display = "none";
            window.location.href = window.location.href;
        }
    };
    XML.open("GET", "http://" + Api_Address + "/AllUsers/EditUser/" + Edit_User_Id + "/" + user_status.value + "/" + user_type.value);
    XML.send();

}
//Get OnClick Edit User Event End








