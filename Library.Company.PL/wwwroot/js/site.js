// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.querySelectorAll('.hover-card').forEach(card => {
    card.addEventListener('mouseenter', () => {
        card.classList.add('shadow', 'scale-up');
    });
    card.addEventListener('mouseleave', () => {
        card.classList.remove('shadow', 'scale-up');
    });
});