document.addEventListener("DOMContentLoaded", function() {
const slidePage = document.querySelector(".slide-page");
const kycslidePage = document.querySelector(".kyc-slide-page");
const nextBtnFirst = document.querySelector(".firstNext");
const kycnextBtnFirst = document.querySelector(".kyc-firstNext");
const prevBtnSec = document.querySelector(".prev-1");
const kycprevBtnSec = document.querySelector(".kyc-prev-1");
const nextBtnSec = document.querySelector(".next-1");
const kycnextBtnSec = document.querySelector(".kyc-next-1");
const prevBtnThird = document.querySelector(".prev-2");
const kycprevBtnThird = document.querySelector(".kyc-prev-2");
const nextBtnThird = document.querySelector(".next-2");
const kycnextBtnThird = document.querySelector(".kyc-next-2");
const prevBtnFourth = document.querySelector(".prev-3");
const kycprevBtnFourth = document.querySelector(".kyc-prev-3");
const submitBtnServiceProvider = document.querySelector("#submitServiceProvider");
const submitBtnKYCAccount = document.querySelector("#submitKYCAccount");
const options = document.querySelectorAll('.option');
const formGroups = document.querySelectorAll('.form-group');

const progressText = document.querySelectorAll(".step p");
const progressCheck = document.querySelectorAll(".step .check");
const bullet = document.querySelectorAll(".step .bullet");
const kycbullet = document.querySelectorAll(".step .kycbullet");
let current = 1;
let kyccurrent = 1;



nextBtnFirst.addEventListener("click", function(event){
  event.preventDefault();
  slidePage.style.marginLeft = "-25%";
  bullet[current - 1].classList.add("active");
  progressCheck[current - 1].classList.add("active");
  progressText[current - 1].classList.add("active");
  current += 1;
});
nextBtnSec.addEventListener("click", function(event){
  event.preventDefault();
  slidePage.style.marginLeft = "-50%";
  bullet[current - 1].classList.add("active");
  progressCheck[current - 1].classList.add("active");
  progressText[current - 1].classList.add("active");
  current += 1;
});
nextBtnThird.addEventListener("click", function(event){
  event.preventDefault();
  slidePage.style.marginLeft = "-75%";
  bullet[current - 1].classList.add("active");
  progressCheck[current - 1].classList.add("active");
  progressText[current - 1].classList.add("active");
  current += 1;
});






prevBtnSec.addEventListener("click", function(event){
  event.preventDefault();
  slidePage.style.marginLeft = "0%";
  bullet[current - 2].classList.remove("active");
  progressCheck[current - 2].classList.remove("active");
  progressText[current - 2].classList.remove("active");
  current -= 1;
});
prevBtnThird.addEventListener("click", function(event){
  event.preventDefault();
  slidePage.style.marginLeft = "-25%";
  bullet[current - 2].classList.remove("active");
  progressCheck[current - 2].classList.remove("active");
  progressText[current - 2].classList.remove("active");
  current -= 1;
});
prevBtnFourth.addEventListener("click", function(event){
  event.preventDefault();
  slidePage.style.marginLeft = "-50%";
  bullet[current - 2].classList.remove("active");
  progressCheck[current - 2].classList.remove("active");
  progressText[current - 2].classList.remove("active");
  current -= 1;
});

kycprevBtnSec.addEventListener("click", function(event){
  event.preventDefault();
  kycslidePage.style.marginLeft = "0%";
  kycbullet[kyccurrent - 2].classList.remove("active");
  progressCheck[kyccurrent - 2].classList.remove("active");
  progressText[kyccurrent- 2].classList.remove("active");
  kyccurrent -= 1;
});
kycprevBtnThird.addEventListener("click", function(event){
  event.preventDefault();
  kycslidePage.style.marginLeft = "-25%";
  kycbullet[kyccurrent - 2].classList.remove("active");
  progressCheck[kyccurrent - 2].classList.remove("active");
  progressText[kyccurrent - 2].classList.remove("active");
  kyccurrent -= 1;
});
kycprevBtnFourth.addEventListener("click", function(event){
  event.preventDefault();
  kycslidePage.style.marginLeft = "-50%";
  kycbullet[kyccurrent - 2].classList.remove("active");
  progressCheck[kyccurrent - 2].classList.remove("active");
  progressText[kyccurrent - 2].classList.remove("active");
  kyccurrent -= 1;
});

kycnextBtnFirst.addEventListener("click", function(event){
  event.preventDefault();
  kycslidePage.style.marginLeft = "-25%";
  kycbullet[kyccurrent - 1].classList.add("active");
  progressCheck[kyccurrent - 1].classList.add("active");
  progressText[kyccurrent - 1].classList.add("active");
  kyccurrent += 1;
});
kycnextBtnSec.addEventListener("click", function(event){
  event.preventDefault();
  kycslidePage.style.marginLeft = "-50%";
  kycbullet[kyccurrent - 1].classList.add("active");
  progressCheck[kyccurrent - 1].classList.add("active");
  progressText[kyccurrent - 1].classList.add("active");
  kyccurrent += 1;
});
kycnextBtnThird.addEventListener("click", function(event){
  event.preventDefault();
  kycslidePage.style.marginLeft = "-75%";
  kycbullet[kyccurrent - 1].classList.add("active");
  progressCheck[kyccurrent - 1].classList.add("active");
  progressText[kyccurrent - 1].classList.add("active");
  kyccurrent += 1;
});
 
 

  options.forEach(option => {
    option.addEventListener('click', function() {
      const targetForm = this.getAttribute('data-form');
      options.forEach(opt => opt.classList.remove('active'));
      this.classList.add('active');
      formGroups.forEach(form => {
        if (form.classList.contains(targetForm)) {
          form.classList.add('active');
        } else {
          form.classList.remove('active');
        }
      });
    });
  });

    document.getElementById('addDocumentBtn').addEventListener('click', function () {
        document.getElementById('addDocumentSection').style.display = 'block';
    });

    document.getElementById('closeDocumentBtn').addEventListener('click', function () {
        document.getElementById('addDocumentSection').style.display = 'none';
    });

});

