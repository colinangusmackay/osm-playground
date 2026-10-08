#!/usr/bin/env bash
set -euo pipefail

if [[ $# -gt 1 ]]; then
    echo "Usage: $0 [admin-user]" >&2
    echo "  admin-user  PostgreSQL admin user to run commands as (default: postgres)" >&2
    exit 1
fi

admin_user="${1:-postgres}"

sudo -u "$admin_user" createuser osmuser
sudo -u "$admin_user" createdb --encoding=UTF8 --owner=osmuser osm
sudo -u "$admin_user" psql osm --command='CREATE EXTENSION postgis;'
sudo -u "$admin_user" psql osm --command='CREATE EXTENSION hstore;'
