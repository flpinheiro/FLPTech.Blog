Use these rules only when generating commit messages.

Format: <type>[scope]: <summary>

Hard rules:
- Prefer the shortest valid Conventional Commit title
- Use imperative mood: add, fix, update, remove
- Use types only when relevant: feat, fix, docs, style, refactor, perf, test, build, ci, chore, revert
- Keep the first line under 72 characters
- Omit scope unless it adds real value
- Return only the commit title; no body, footer, or explanation unless explicitly requested
- Use BREAKING CHANGE only for true breaking changes

Examples:
feat: add blog post retrieval
fix: correct auth token refresh
docs: update setup instructions