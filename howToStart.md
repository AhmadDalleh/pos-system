# How I start a project

1. Before any code, I create a home for the project: git, a .gitignore, and one sentence explaining what I'm building and why.
2. I push to GitHub right away, into an empty repo, so the project is backed up and reviewable from the first commit.
3. Files that are personal or secret go in .gitignore before the first commit, because ignoring something after it's committed doesn't remove it from history.
4. Before creating projects, I set the shared rules at the root: SDK version, code style, build settings, and package versions, so every project follows them automatically.