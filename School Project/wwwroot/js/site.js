// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const teacherModalButton = document.querySelectorAll('.teacherModalButton');
const teacherModal = document.getElementById('teacherModal');
const closeTeacherModal = document.querySelectorAll('.closeTeacherModal');
console.log(closeTeacherModal);

teacherModalButton.forEach(item => {
    item.addEventListener('click', (event) => {
        event.preventDefault();
        teacherModal.classList.add('show-modal');
    })
})

closeTeacherModal.forEach(item => {
    item.addEventListener('click', (event) => {
        event.preventDefault();
        teacherModal.classList.remove('show-modal');
    })
})
