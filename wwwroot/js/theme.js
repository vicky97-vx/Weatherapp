(function () {
  function applySavedTheme() {
    try {
      var body = document.body;
      if (!body) return;
      var theme = localStorage.getItem('theme');
      if (theme === 'dark') body.classList.add('dark-mode');
      else body.classList.remove('dark-mode');
    } catch { /* noop */ }
  }

  function toggleDarkMode() {
    var body = document.body;
    body.classList.toggle('dark-mode');
    localStorage.setItem('theme', body.classList.contains('dark-mode') ? 'dark' : 'light');
  }

  // expose for Blazor interop
  window.applySavedTheme = applySavedTheme;
  window.toggleDarkMode = toggleDarkMode;

  // initial load (before/after Blazor starts)
  if (document.readyState === 'loading')
    document.addEventListener('DOMContentLoaded', applySavedTheme);
  else
    applySavedTheme();

  // re-apply on SPA navigations (pushState / replaceState / back/forward)
  (function (history) {
    var push = history.pushState, replace = history.replaceState;
    history.pushState = function () {
      var ret = push.apply(this, arguments);
      window.dispatchEvent(new Event('locationchange'));
      return ret;
    };
    history.replaceState = function () {
      var ret = replace.apply(this, arguments);
      window.dispatchEvent(new Event('locationchange'));
      return ret;
    };
  })(window.history);

  window.addEventListener('popstate', function () {
    window.dispatchEvent(new Event('locationchange'));
  });

  window.addEventListener('locationchange', function () {
    // let Blazor finish rendering, then apply
    setTimeout(applySavedTheme, 0);
  });
})();
