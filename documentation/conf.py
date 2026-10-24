# Configuration file for the Sphinx documentation builder.
#
# For the full list of built-in configuration values, see the documentation:
# https://www.sphinx-doc.org/en/master/usage/configuration.html

# -- Project information -----------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#project-information

project = 'DevWorkBench'
copyright = '2026, DevWorkBench'
author = 'DevWorkBench'
release = '0.1'

# -- General configuration ---------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#general-configuration

extensions = ["myst_parser"]
exclude_patterns = ['_build', '.venv', 'Thumbs.db', '.DS_Store']   

myst_enable_extensions = [
    "colon_fence",
    "deflist",
    "dollarmath",
    "amsmath",
    "fieldlist",
    "strikethrough",
    "linkify",
    "smartquotes",
    "substitution",
    "attrs_inline"
]

myst_heading_anchors = 3
myst_number_code_blocks = ["python", "bash"]

myst_substitutions = {
    "project": "DevWorkBench",
    "version": "0.1"
}

templates_path = ['_templates']

# -- Options for HTML output -------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#options-for-html-output

html_theme = 'alabaster'
html_static_path = ['_static']   